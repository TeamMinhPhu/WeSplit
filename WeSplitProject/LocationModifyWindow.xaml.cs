using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeSplitProject
{
    /// <summary>
    /// Interaction logic for LocationModifyWindow.xaml
    /// </summary>
    public partial class LocationModifyWindow : Window
    {
        DateTime? beginDate;
        DateTime? finishDate;

        VISIT_LOCATION myVisitLoc;
        public VISIT_LOCATION newVisitLoc { get; set; }

        string _visitLocImageLink;

        WeSplitDBEntities db = new WeSplitDBEntities();

        public LocationModifyWindow(VISIT_LOCATION tempVisitLoc, DateTime? tempBeginDate, DateTime? tempFinishDate)
        {
            InitializeComponent();
            myVisitLoc = tempVisitLoc;
            beginDate = tempBeginDate;
            finishDate = tempFinishDate;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            visitLocDestinationTextBox.Text = myVisitLoc.VISIT_LOC_DESTINATION;
            visitLocDateBeginDatePicker.SelectedDate = myVisitLoc.DATE_BEGIN;
            visitLocDateFinishDatePicker.SelectedDate = myVisitLoc.DATE_FINISH;
            visitLocDescriptionTextBox.Text = myVisitLoc.VISIT_LOC_DESCRIPTION;

            visitLocDateBeginDatePicker.DisplayDateStart = beginDate;
            visitLocDateBeginDatePicker.DisplayDateEnd = finishDate;
            visitLocDateFinishDatePicker.DisplayDateStart = beginDate;
            visitLocDateFinishDatePicker.DisplayDateEnd = finishDate;

            try
            {
                if (myVisitLoc.IMAGE_LINK.Length > 0)
                {
                    _visitLocImageLink = myVisitLoc.IMAGE_LINK;
                    var Bitmap = new BitmapImage(new Uri(_visitLocImageLink, UriKind.Absolute));
                    visitLocImage.Source = Bitmap;
                    visitLocImageHint.Visibility = Visibility.Hidden;
                }
                else
                {
                    _visitLocImageLink = "";
                }
            }
            catch
            {
                MessageBox.Show("Không mở được thông tin địa điểm", "Lỗi");
                this.Close();
            }

            MouseDown += Window_MouseDown;
        }

        //drag window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static bool IsImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            bool result = false;
            if (!String.IsNullOrEmpty(targetExtension))
            {
                List<string> recognisedImageExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".tiff", ".ico" };
                foreach (string extension in recognisedImageExtensions)
                {
                    if (extension.Equals(targetExtension))
                    {
                        result = true;
                        break;
                    }
                }
            }
            else { /*do nothing*/ }
            return result;
        }

        private void visitLocImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                _visitLocImageLink = dlg.FileName;
                if (IsImageFile(_visitLocImageLink))
                {
                    var Bitmap = new BitmapImage(new Uri(_visitLocImageLink, UriKind.Absolute));
                    visitLocImage.Source = Bitmap;
                    visitLocImageHint.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                    _visitLocImageLink = "";
                }
            }
        }

        private void visitLocImage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Get file link
                string[] ImageFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                //check image file and show image
                if (ImageFiles[0].Length > 0)
                {
                    _visitLocImageLink = ImageFiles[0];
                    if (IsImageFile(_visitLocImageLink))
                    {
                        var Bitmap = new BitmapImage(new Uri(_visitLocImageLink, UriKind.Absolute));
                        visitLocImage.Source = Bitmap;
                        visitLocImageHint.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Không mở được ảnh");
                        _visitLocImageLink = "";
                    }
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                }

            }
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (visitLocDestinationTextBox.Text.Length > 0)
            {
                myVisitLoc.VISIT_LOC_DESTINATION = visitLocDestinationTextBox.Text;
                myVisitLoc.VISIT_LOC_DESCRIPTION = visitLocDescriptionTextBox.Text;
                myVisitLoc.DATE_BEGIN = visitLocDateBeginDatePicker.SelectedDate;
                myVisitLoc.DATE_FINISH = visitLocDateFinishDatePicker.SelectedDate;
                myVisitLoc.IMAGE_LINK = _visitLocImageLink;

                newVisitLoc = myVisitLoc;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Chưa chọn địa điểm tham quan", "Lỗi");
            }

        }

        private void visitLocDateBeginDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var visitLocBeginDate = visitLocDateBeginDatePicker.SelectedDate;
            var visitLocFinishDate = visitLocDateFinishDatePicker.SelectedDate;

            if (visitLocBeginDate != null)
            {
                if (beginDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày bắt đầu chuyến đi", "Cảnh báo");
                    visitLocDateBeginDatePicker.SelectedDate = null;
                }
                else if (finishDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày kết thúc chuyến đi", "Cảnh báo");
                    visitLocDateBeginDatePicker.SelectedDate = null;
                }
                else
                {
                    if (visitLocFinishDate != null)
                    {
                        if (visitLocBeginDate != null)
                        {
                            if (visitLocBeginDate > visitLocFinishDate)
                            {
                                MessageBox.Show("Ngày bắt đầu sau ngày kết thúc", "Lỗi");
                                visitLocDateBeginDatePicker.SelectedDate = null;
                            }
                        }
                    }
                }
            }

        }

        private void visitLocDateFinishDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var visitLocBeginDate = visitLocDateBeginDatePicker.SelectedDate;
            var visitLocFinishDate = visitLocDateFinishDatePicker.SelectedDate;

            if (visitLocFinishDate != null)
            {
                if (finishDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày kết thúc chuyến đi", "Cảnh báo");
                    visitLocDateFinishDatePicker.SelectedDate = null;
                }
                else if (beginDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày bắt đầu chuyến đi", "Cảnh báo");
                    visitLocDateFinishDatePicker.SelectedDate = null;
                }
                else
                {


                    if (visitLocBeginDate != null)
                    {
                        if (visitLocFinishDate != null)
                        {
                            if (visitLocBeginDate > visitLocFinishDate)
                            {
                                MessageBox.Show("Ngày bắt đầu sau ngày kết thúc", "Lỗi");
                                visitLocDateBeginDatePicker.SelectedDate = null;
                            }
                        }
                    }
                }
            }

        }
    }
}
