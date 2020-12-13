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
    /// Interaction logic for CreateNewTrip.xaml
    /// </summary>
    public partial class CreateNewTrip : Window
    {
        class TripDestination
        {
            public string Destination { get; set; }
        }

        class ExpenseInput
        {
            public TextBox ExpenseTB { get; set; }
            public TextBox CostTB { get; set; }
            public bool ExpenseTBCheck { get; set; }
            public bool CostTBCheck { get; set; }
            public long Cost { get; set; }
        }

        class MemberView
        {
            public MEMBER member { get; set; }
            public int totalExpense { get; set; }
        }

        class MemberExpense
        {
            public string expense { get; set; }
            public long cost { get; set; }
        }

        string _tripImageLink;
        string _visitLocImageLink;
        string _avatarImageLink;
        int memberCode;
        int visitLocCode;

        TRIP myTrip;
        BindingList<EXPENSE> myExpense;
        BindingList<VISIT_LOCATION> myVisitLoc;
        List<TRIP_SPLIT> myTripSplit;
        BindingList<MEMBER> myMember;
        BindingList<TripDestination> myTripDes;
        BindingList<TripDestination> myVisitDes;
        BindingList<MemberView> myMemberView;
        List<MemberExpense> memberExpenseList;
        List<ExpenseInput> myExpenseInput;

        long expenseTotalCost;

        WeSplitDBEntities db = new WeSplitDBEntities();



        public CreateNewTrip()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _tripImageLink = "";
            _visitLocImageLink = "";
            _avatarImageLink = "";
            expenseTotalCost = 0;
            visitLocCode = 0;
            memberCode = 0;

            myExpenseInput = new List<ExpenseInput>();

            myVisitLoc = new BindingList<VISIT_LOCATION>();
            vitsitLocList.ItemsSource = myVisitLoc;

            myTripSplit = new List<TRIP_SPLIT>();
            memberExpenseList = new List<MemberExpense>();



            //Add recommend destination
            var query_TripDes = db.TRIPs.OrderBy(c => c.TRIP_DESTINATION).Select(c => c.TRIP_DESTINATION).Distinct().ToList();
            myTripDes = new BindingList<TripDestination>();
            foreach (var item in query_TripDes)
            {
                myTripDes.Add(new TripDestination { Destination = item });
            }
            // add to combobox
            tripDestinationComboBox.ItemsSource = myTripDes;

            //Add recommend visit location
            var query_visitLocDes = db.VISIT_LOCATION.OrderBy(c => c.VISIT_LOC_DESTINATION).Select(c => c.VISIT_LOC_DESTINATION).Distinct().ToList();
            myVisitDes = new BindingList<TripDestination>();
            foreach (var item in query_visitLocDes)
            {
                myVisitDes.Add(new TripDestination { Destination = item });
            }

            //Add to combobox
            visitLocDestinationComboBox.ItemsSource = myVisitDes;

            //Create member listbox source
            myMember = new BindingList<MEMBER>();
            memberListBox.ItemsSource = myMember;
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

        private void TripImage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Get file link
                string[] ImageFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                //check image file and show image
                if (ImageFiles[0].Length > 0)
                {
                    _tripImageLink = ImageFiles[0];
                    if (IsImageFile(_tripImageLink))
                    {
                        var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
                        tripImage.Source = Bitmap;
                        tripImageHint.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Không mở được ảnh");
                        _tripImageLink = "";
                    }
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                }

            }
        }

        private void TripImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                _tripImageLink = dlg.FileName;
                if (IsImageFile(_tripImageLink))
                {
                    var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
                    tripImage.Source = Bitmap;
                    tripImageHint.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                    _tripImageLink = "";
                }
            }
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

        private void addExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            int code = myExpenseInput.Count;

            //Horizontal Stack panel
            StackPanel subExpenseSP = new StackPanel();
            subExpenseSP.Orientation = Orientation.Horizontal;

            //Description textbox
            TextBox expenseDesTextBox = new TextBox();
            expenseDesTextBox.Name = $"E{code}";
            expenseDesTextBox.Width = 180;
            expenseDesTextBox.FontSize = 16;
            expenseDesTextBox.Margin = new Thickness(0, 10, 5, 0);
            MaterialDesignThemes.Wpf.HintAssist.SetHint(expenseDesTextBox, "Mô tả");
            expenseDesTextBox.LostFocus += ExpenseDesTextBox_LostFocus;

            //Cost textbox
            TextBox CostTextBox = new TextBox();
            CostTextBox.Name = $"C{code}";
            CostTextBox.Width = 100;
            CostTextBox.FontSize = 16;
            CostTextBox.Margin = new Thickness(5, 10, 0, 0);
            MaterialDesignThemes.Wpf.HintAssist.SetHint(CostTextBox, "Chi phí (VNĐ)");
            CostTextBox.LostFocus += CostTextBox_LostFocus;

            subExpenseSP.Children.Add(expenseDesTextBox);
            subExpenseSP.Children.Add(CostTextBox);

            myExpenseInput.Add(new ExpenseInput { ExpenseTB = expenseDesTextBox, CostTB = CostTextBox, CostTBCheck = false, ExpenseTBCheck = false, Cost = 0 });

            SPExpense.Children.Add(subExpenseSP);
        }

        private void addVisitLocBtn_Click(object sender, RoutedEventArgs e)
        {
            string _selectedItem = "";

            _selectedItem = visitLocDestinationComboBox.Text;

            if (_selectedItem.Length > 0)
            {
                if (visitLocDateBeginDatePicker.SelectedDate == null)
                {
                    if (MessageBox.Show("Chưa chọn ngày bắt đầu, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (visitLocDateFinishDatePicker.SelectedDate == null)
                {
                    if (MessageBox.Show("Chưa chọn ngày kết thúc, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (visitLocDescriptionTextBox.Text.Length <= 0)
                {
                    if (MessageBox.Show("Chưa nhập mô tả điểm tham quan, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (_visitLocImageLink.Length <= 0)
                {
                    if (MessageBox.Show("Chưa chọn hình điểm tham quan, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                myVisitLoc.Add(new VISIT_LOCATION { VISIT_LOC_ID = $"VL{visitLocCode}", VISIT_LOC_DESTINATION = _selectedItem, DATE_BEGIN = visitLocDateBeginDatePicker.SelectedDate, DATE_FINISH = visitLocDateFinishDatePicker.SelectedDate, VISIT_LOC_DESCRIPTION = visitLocDescriptionTextBox.Text, IMAGE_LINK = _visitLocImageLink });
                visitLocDestinationComboBox.Text = "";
                visitLocDescriptionTextBox.Text = "";
                visitLocDateBeginDatePicker.SelectedDate = null;
                visitLocDateFinishDatePicker.SelectedDate = null;
                visitLocCode++;
                for (int i = myVisitDes.Count - 1; i >= 0; i--)
                {
                    if (myVisitDes[i].Destination == _selectedItem)
                    {
                        return;
                    }
                }
                myVisitDes.Add(new TripDestination { Destination = _selectedItem });
            }
            else
            {
                MessageBox.Show("Lỗi: chưa chọn địa điểm tham quan");
            }


        }

        private void deleteVisitLocBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = vitsitLocList.SelectedIndex;
            if (index >= 0)
            {
                myVisitLoc.RemoveAt(index);
            }
        }

        private void addMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (memberNameTextBox.Text.Length <= 0)
            {
                MessageBox.Show("Chưa nhập tên thành viên", "Lỗi");
            }
            else
            {
                if (memberPhoneTextBox.Text.Length <= 0)
                {
                    if (MessageBox.Show("Chưa nhập số điện thoại, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (memberEmailTextBox.Text.Length <= 0)
                {
                    if (MessageBox.Show("Chưa nhập email, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (_avatarImageLink.Length <= 0)
                {
                    if (MessageBox.Show("Chưa chọn ảnh đại diện, Bạn có muốn tiếp tục không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                myMember.Add(new MEMBER { MEMBER_ID = $"M{memberCode}", MEMBER_NAME = memberNameTextBox.Text, PHONE = memberPhoneTextBox.Text, EMAIL = memberEmailTextBox.Text, AVATAR = _avatarImageLink });
                memberNameTextBox.Text = "";
                memberPhoneTextBox.Text = "";
                memberEmailTextBox.Text = "";
                var Bitmap = new BitmapImage(new Uri("Resources/Icons/picture.png", UriKind.Relative));
                avatarImage.Source = Bitmap;
                avatarImageHint.Visibility = Visibility.Visible;
                _avatarImageLink = "";
                memberCode++;
            }

        }

        private void deleteMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = memberListBox.SelectedIndex;
            if (index >= 0)
            {
                myMember.RemoveAt(index);
            }
        }

        private void ExpenseDesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            int flag = 0;
            //find expense id
            for (int i = 0; i < myExpenseInput.Count; i++)
            {
                if (textbox.Name == $"E{i}")
                {
                    flag = i;
                    break;
                }
            }

            if (myExpenseInput[flag].ExpenseTBCheck)
            {
                if (textbox.Text.Length <= 0)
                {
                    if (myExpenseInput[flag].CostTBCheck)
                    {
                        expenseTotalCost -= myExpenseInput[flag].Cost;
                    }
                    myExpenseInput[flag].ExpenseTBCheck = false;
                }
            }
            else
            {
                if (textbox.Text.Length > 0)
                {
                    if (myExpenseInput[flag].CostTBCheck)
                    {
                        try
                        {
                            expenseTotalCost += myExpenseInput[flag].Cost;
                        }
                        catch
                        {
                            MessageBox.Show("Số tiền vượt quá giới hạn", "Lỗi");
                            myExpenseInput[flag].ExpenseTBCheck = false;
                            return;
                        }
                    }
                    myExpenseInput[flag].ExpenseTBCheck = true;
                }
            }
            totalExpenseTextBlock.Text = $"Tổng chi phí: {expenseTotalCost.ToString()}";
        }

        private void CostTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;

            int flag = 0;
            //find expense id
            for (int i = 0; i < myExpenseInput.Count; i++)
            {
                if (textbox.Name == $"C{i}")
                {
                    flag = i;
                    break;
                }
            }

            bool checkLong = false;
            long result = 0;

            if (textbox.Text.Length > 0)
            {
                checkLong = long.TryParse(textbox.Text, out result);
                if (!checkLong)
                {
                    MessageBox.Show("Lỗi: Số tiền nhập không phải là số hoặc vượt quá giới hạn");
                }
            }


            if (myExpenseInput[flag].CostTBCheck)
            {
                if (!checkLong)
                {
                    if (myExpenseInput[flag].ExpenseTBCheck)
                    {
                        expenseTotalCost -= myExpenseInput[flag].Cost;
                    }
                    myExpenseInput[flag].CostTBCheck = false;
                    myExpenseInput[flag].Cost = 0;
                }
                else
                {
                    if (myExpenseInput[flag].ExpenseTBCheck)
                    {
                        try
                        {
                            expenseTotalCost = expenseTotalCost - myExpenseInput[flag].Cost + result;
                        }
                        catch
                        {
                            MessageBox.Show("Số tiền vượt quá giới hạn", "Lỗi");
                            myExpenseInput[flag].CostTBCheck = false;
                            return;
                        }
                    }
                    myExpenseInput[flag].CostTBCheck = true;
                    myExpenseInput[flag].Cost = result;
                }
            }
            else
            {
                if (checkLong)
                {
                    if (myExpenseInput[flag].ExpenseTBCheck)
                    {
                        try
                        {
                            expenseTotalCost = expenseTotalCost - myExpenseInput[flag].Cost + result;
                        }
                        catch
                        {
                            MessageBox.Show("Số tiền vượt quá giới hạn", "Lỗi");
                            myExpenseInput[flag].CostTBCheck = false;
                            return;
                        }
                    }
                    myExpenseInput[flag].CostTBCheck = true;
                    myExpenseInput[flag].Cost = result;
                }
            }

            totalExpenseTextBlock.Text = $"Tổng chi phí: {expenseTotalCost.ToString()}";
        }

        private void memberListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = (sender as ListBox).SelectedIndex;
            var memberEditScreen = new MemberModifyWindow(myMember[index], myTripSplit);
            if (memberEditScreen.ShowDialog() == true)
            {
                var tempTripSplit = memberEditScreen.newTripSplit;
                myTripSplit.RemoveAll(c => c.MEMBER_ID == myMember[index].MEMBER_ID);

                for (int i = 0; i < tempTripSplit.Count; i++)
                {
                    myTripSplit.Add(tempTripSplit[i]);
                }

                var tempMember = memberEditScreen.newMember;
                myMember.RemoveAt(index);
                myMember.Add(tempMember);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void vitsitLocList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = (sender as ListBox).SelectedIndex;
            var visitLocEditScreen = new LocationModifyWindow(myVisitLoc[index]);
            if (visitLocEditScreen.ShowDialog() == true)
            {
                var tempVisitLoc = visitLocEditScreen.newVisitLoc;
                myVisitLoc.RemoveAt(index);
                myVisitLoc.Add(tempVisitLoc);
            }
        }
    }
}
