using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.IO;
using System.Globalization;

using MaterialDesignThemes;
using MaterialDesignColors;
using Microsoft.Win32;

namespace WeSplitProject
{
    /// <summary>
    /// Interaction logic for MemberModifyWindow.xaml
    /// </summary>
    public partial class MemberModifyWindow : Window
    {
        BindingList<TRIP_SPLIT> myTripSplit;
        public List<TRIP_SPLIT> newTripSplit { get; set; }

        MEMBER myMember;
        public MEMBER newMember { get; set; }

        string _avatarImageLink;
        long totalCost;

        public MemberModifyWindow(MEMBER tempMember, List<TRIP_SPLIT> tempTripSplit)
        {
            InitializeComponent();
            myMember = tempMember;
            myTripSplit = new BindingList<TRIP_SPLIT>(tempTripSplit.Where(c => c.MEMBER_ID == myMember.MEMBER_ID).ToList());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            memberNameTextBox.Text = myMember.MEMBER_NAME;
            memberPhoneTextBox.Text = myMember.PHONE;
            memberEmailTextBox.Text = myMember.EMAIL;

            totalCost = 0;

            if (myMember.AVATAR.Length > 0)
            {
                _avatarImageLink = myMember.AVATAR;
                var Bitmap = new BitmapImage(new Uri(_avatarImageLink, UriKind.Absolute));
                avatarImage.Source = Bitmap;
                avatarImageHint.Visibility = Visibility.Hidden;
            }
            else
            {
                _avatarImageLink = "";
            }

            expenseListBox.ItemsSource = myTripSplit;
            if (myTripSplit.Count > 0)
            {
                expenseSPHint.Visibility = Visibility.Visible;
                totalExpenseTextBlock.Visibility = Visibility.Visible;
                for (int i = 0; i < myTripSplit.Count; i++)
                {
                    try
                    {
                        totalCost += Convert.ToInt64(myTripSplit[i].PAID_COST);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                totalExpenseTextBlock.Text = $"Tổng: {totalCost}";
            }
            MouseDown += Window_MouseDown;
        }

        //drag window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
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

        private void avatarImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                _avatarImageLink = dlg.FileName;
                if (IsImageFile(_avatarImageLink))
                {
                    var Bitmap = new BitmapImage(new Uri(_avatarImageLink, UriKind.Absolute));
                    avatarImage.Source = Bitmap;
                    avatarImageHint.Visibility = Visibility.Hidden;

                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                    _avatarImageLink = "";
                }
            }
        }

        private void avatarImage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Get file link
                string[] ImageFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                //check image file and show image
                if (ImageFiles[0].Length > 0)
                {
                    _avatarImageLink = ImageFiles[0];
                    if (IsImageFile(_avatarImageLink))
                    {
                        var Bitmap = new BitmapImage(new Uri(_avatarImageLink, UriKind.Absolute));
                        avatarImage.Source = Bitmap;
                        avatarImageHint.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Không mở được ảnh");
                        _avatarImageLink = "";
                    }
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                }

            }
        }


        private void deleteExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = expenseListBox.SelectedIndex;
            if (index >= 0)
            {
                totalCost -= Convert.ToInt64(myTripSplit[index].PAID_COST);
                totalExpenseTextBlock.Text = $"Tổng: {totalCost}";

                myTripSplit.RemoveAt(index);
                if (myTripSplit.Count <= 0)
                {
                    expenseSPHint.Visibility = Visibility.Collapsed;
                    totalExpenseTextBlock.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void addExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (paymentTextBox.Text.Length <= 0)
            {
                if (MessageBox.Show("Chưa nhập mô tả khoản chi, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
            }


            if (CostTextBox.Text.Length > 0)
            {
                if (!long.TryParse(CostTextBox.Text, out long result))
                {
                    MessageBox.Show("Số tiền nhập không phải là số hoặc vượt quá giới hạn", "Lỗi");
                }
                else
                {
                    try
                    {
                        totalCost += result;
                    }
                    catch
                    {
                        MessageBox.Show("Số tiền vượt quá giới hạn", "Lỗi");
                        return;
                    }
                    myTripSplit.Add(new TRIP_SPLIT { MEMBER_ID = myMember.MEMBER_ID, PAYMENT_DESCRIPTION = paymentTextBox.Text, PAID_COST = result });
                    paymentTextBox.Text = "";
                    CostTextBox.Text = "";

                    totalExpenseTextBlock.Text = $"Tổng: {totalCost}";
                    expenseSPHint.Visibility = Visibility.Visible;
                    totalExpenseTextBlock.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Chưa nhập số tiền", "Lỗi");
            }

        }


        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (memberNameTextBox.Text.Length <= 0)
            {
                MessageBox.Show("Chưa nhập tên thành viên", "Lỗi");
            }
            else
            {
                myMember.MEMBER_NAME = memberNameTextBox.Text;
                myMember.PHONE = memberPhoneTextBox.Text;
                myMember.EMAIL = memberEmailTextBox.Text;
                myMember.AVATAR = _avatarImageLink;

                newMember = myMember;

                var temp = new List<TRIP_SPLIT>();
                for (int i = 0; i < myTripSplit.Count; i++)
                {
                    temp.Add(new TRIP_SPLIT { MEMBER_ID = myMember.MEMBER_ID, PAYMENT_ID = $"TS{i}", PAYMENT_DESCRIPTION = myTripSplit[i].PAYMENT_DESCRIPTION, PAID_COST = myTripSplit[i].PAID_COST });
                }
                newTripSplit = temp;

                DialogResult = true;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
