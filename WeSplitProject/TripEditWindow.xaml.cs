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

using WeSplitProject.Classes;

namespace WeSplitProject
{
    /// <summary>
    /// Interaction logic for CreateNewTrip.xaml
    /// </summary>
    public partial class CreateNewTrip : Window
    {
        class ExpenseInput
        {
            public TextBox ExpenseTB { get; set; }
            public TextBox CostTB { get; set; }
            public bool ExpenseTBCheck { get; set; }
            public bool CostTBCheck { get; set; }
            public long Cost { get; set; }
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

        string _TripID;
        TRIP myTrip;

        BindingList<VISIT_LOCATION> myVisitLoc;
        BindingList<MEMBER> myMember;

        List<MemberExpense> memberExpenseList;
        List<ExpenseInput> myExpenseInput;
        List<EXPENSE> myExpense;
        List<TRIP_SPLIT> myTripSplit;

        long expenseTotalCost;

        WeSplitDBEntities db = new WeSplitDBEntities();

        int mode; //mode 0: create new trip; mode 1: update trip

        public CreateNewTrip()
        {
            InitializeComponent();
            mode = 0;
        }

        public CreateNewTrip(string tempTripId)
        {
            InitializeComponent();
            _TripID = tempTripId;
            mode = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 1)
            {
                if (_TripID.Length > 0)
                {
                    //Load trip from database
                    try
                    {
                        _tripImageLink = "";
                        _visitLocImageLink = "";
                        _avatarImageLink = "";
                        expenseTotalCost = 0;
                        visitLocCode = 0;
                        memberCode = 0;

                        var Folder = AppDomain.CurrentDomain.BaseDirectory;

                        //load trip
                        myTrip = db.TRIPs.Where(c => c.TRIP_ID == _TripID).FirstOrDefault();

                        //show trip
                        tripNameTextBox.Text = myTrip.TRIP_NAME;
                        tripDestinationTextBox.Text = myTrip.TRIP_DESTINATION;
                        tripDescriptionTextBox.Text = myTrip.TRIP_DESCRIPTION;
                        tripStatusComboBox.SelectedIndex = myTrip.TRIP_STATUS.Value;
                        tripDateBeginDatePicker.SelectedDate = myTrip.DATE_BEGIN;
                        tripDateFinishDatePicker.SelectedDate = myTrip.DATE_FINISH;

                        if (myTrip.IMAGE_LINK.Length > 0)
                        {                            
                            _tripImageLink = $"{Folder}{myTrip.IMAGE_LINK}";                            
                            var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
                            tripImage.Source = Bitmap;
                            tripImageHint.Visibility = Visibility.Hidden;
                        }
                        else
                        {/*do nothing*/}

                        //Load Main Expense
                        myExpense = db.EXPENSEs.Where(c => c.TRIP_ID == _TripID).ToList();

                        //Create Input from and present data
                        myExpenseInput = new List<ExpenseInput>();
                        for (int i = 0; i < myExpense.Count; i++)
                        {
                            AddExpenseInputSpace();
                            if (myExpenseInput.Count == i + 1)
                            {
                                myExpenseInput[i].ExpenseTB.Text = myExpense[i].EXPENSE_DESCRIPTION;
                                myExpenseInput[i].ExpenseTBCheck = true;                                
                                myExpenseInput[i].CostTBCheck = true;
                                myExpenseInput[i].Cost = Convert.ToInt64(myExpense[i].COST);
                                myExpenseInput[i].CostTB.Text = $"{myExpenseInput[i].Cost}";

                                expenseTotalCost += myExpenseInput[i].Cost;
                            }
                        }
                        totalExpenseTextBlock.Text = $"Tổng chi phí: {expenseTotalCost.ToString()}";

                        //Load visit Location data
                        myVisitLoc = new BindingList<VISIT_LOCATION>(db.VISIT_LOCATION.Where(c => c.TRIP_ID == _TripID).ToList());
                        for (int i = 0; i < myVisitLoc.Count; i++)
                        {
                            if (myVisitLoc[i].IMAGE_LINK.Length > 0)
                            {
                                myVisitLoc[i].IMAGE_LINK = Folder + myVisitLoc[i].IMAGE_LINK;
                            }
                        }
                        vitsitLocList.ItemsSource = myVisitLoc;
                        visitLocCode = myVisitLoc.Count;

                        //Load member data
                        myMember = new BindingList<MEMBER>(db.MEMBERs.Where(c => c.TRIP_ID == _TripID).ToList());
                        for (int i = 0; i < myMember.Count; i++)
                        {
                            if (myMember[i].AVATAR.Length > 0)
                            {
                                myMember[i].AVATAR = Folder + myMember[i].AVATAR;
                            }
                        }
                        memberListBox.ItemsSource = myMember;
                        memberCode = myMember.Count;

                        //Load Trip Split
                        myTripSplit = db.TRIP_SPLIT.Where(c => c.TRIP_ID == _TripID).ToList();
                        for (int i = 0; i < myTripSplit.Count; i++)
                        {
                            if(myTripSplit[i].PAID_COST != null)
                            {
                                myTripSplit[i].PAID_COST = Convert.ToInt64(myTripSplit[i].PAID_COST);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Không tìm thấy thông tin chuyến đi", "Lỗi");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin chuyến đi", "Lỗi");
                    this.Close();
                }
            }
            else if (mode == 0)
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

                //Create member listbox source
                myMember = new BindingList<MEMBER>();
                memberListBox.ItemsSource = myMember;
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
            AddExpenseInputSpace();
        }

        private void AddExpenseInputSpace()
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

            _selectedItem = visitLocDestinationTextBox.Text;

            if (_selectedItem.Length > 0)
            {
                myVisitLoc.Add(new VISIT_LOCATION { VISIT_LOC_ID = $"VL{visitLocCode}", VISIT_LOC_DESTINATION = _selectedItem, DATE_BEGIN = visitLocDateBeginDatePicker.SelectedDate, DATE_FINISH = visitLocDateFinishDatePicker.SelectedDate, VISIT_LOC_DESCRIPTION = visitLocDescriptionTextBox.Text, IMAGE_LINK = _visitLocImageLink });
                visitLocDestinationTextBox.Text = "";
                visitLocDescriptionTextBox.Text = "";
                visitLocDateBeginDatePicker.SelectedDate = null;
                visitLocDateFinishDatePicker.SelectedDate = null;
                visitLocCode++;
                var Bitmap = new BitmapImage(new Uri("Resources/Icons/picture.png", UriKind.Relative));
                visitLocImage.Source = Bitmap;
                _visitLocImageLink = "";
                visitLocImageHint.Visibility = Visibility.Visible;

                /*for (int i = myVisitDes.Count - 1; i >= 0; i--) 
                {
                    if(myVisitDes[i].Destination == _selectedItem)
                    {
                        return;
                    }
                }
                myVisitDes.Add(new TripDestination { Destination = _selectedItem });*/
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
            ExpenseDesTextBoxHandler(textbox);
        }

        private void ExpenseDesTextBoxHandler(TextBox textbox)
        {
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
            CostTextBoxHandler(textbox);
        }

        private void CostTextBoxHandler(TextBox textbox)
        {
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
            var visitLocEditScreen = new LocationModifyWindow(myVisitLoc[index], tripDateBeginDatePicker.SelectedDate, tripDateFinishDatePicker.SelectedDate);
            if (visitLocEditScreen.ShowDialog() == true)
            {
                var tempVisitLoc = visitLocEditScreen.newVisitLoc;
                myVisitLoc.RemoveAt(index);
                myVisitLoc.Add(tempVisitLoc);
            }
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tripNameTextBox.Text.Length <= 0)
            {
                MessageBox.Show("Chưa nhập tên chuyến đi", "Cảnh báo");
            }
            else
            {
                if (tripDestinationTextBox.Text.Length <= 0)
                {
                    MessageBox.Show("Chưa nhập địa điểm tham quan", "Cảnh báo");
                }
                else
                {
                    var statusIndex = tripStatusComboBox.SelectedIndex;
                    if (statusIndex < 0)
                    {
                        MessageBox.Show("Chưa chọn trạng thái chuyến đi", "Cảnh báo");
                    }
                    else
                    {
                        if (mode == 1)
                        {
                            //Delete old data
                            //Detele on VISIT_LOCATION
                            db.VISIT_LOCATION.RemoveRange(db.VISIT_LOCATION.Where(c => c.TRIP_ID == _TripID).ToList());

                            //Detele on TRIP_SPLIT
                            db.TRIP_SPLIT.RemoveRange(db.TRIP_SPLIT.Where(c => c.TRIP_ID == _TripID).ToList());

                            //Detele on MEMBER
                            db.MEMBERs.RemoveRange(db.MEMBERs.Where(c => c.TRIP_ID == _TripID).ToList());

                            //Detele on EXPENSE
                            db.EXPENSEs.RemoveRange(db.EXPENSEs.Where(c => c.TRIP_ID == _TripID).ToList());

                            //Delete on TRIP
                            db.TRIPs.Remove(myTrip);

                            db.SaveChanges();

                            //Create folder to save image
                            var Folder = AppDomain.CurrentDomain.BaseDirectory;
                            var savedFolderLink = $"Resources\\Images\\{_TripID}";
                            MyFileManager.CheckDictionary($"{Folder}{savedFolderLink}");

                            //Save trip
                            SaveNewTrip(_TripID, savedFolderLink);

                            //Save expense
                            SaveExpense(_TripID);

                            //Save Visit Location
                            SaveVisitLocation(_TripID, savedFolderLink);

                            //Save Member and tripsplit
                            SaveMember(_TripID, savedFolderLink);
                        }
                        else if (mode == 0)
                        {
                            var tempTripIds = db.TRIPs.Select(c => c.TRIP_ID).ToList();
                            string myTripId = $"TRIP{tempTripIds.Count}";

                            //Create folder to save image
                            var Folder = AppDomain.CurrentDomain.BaseDirectory;
                            var savedFolderLink = $"Resources\\Images\\{myTripId}";
                            MyFileManager.CheckDictionary($"{Folder}{savedFolderLink}");

                            //Save trip
                            SaveNewTrip(myTripId, savedFolderLink);

                            //Save expense
                            SaveExpense(myTripId);

                            //Save Visit Location
                            SaveVisitLocation(myTripId, savedFolderLink);

                            //Save Member and tripsplit
                            SaveMember(myTripId, savedFolderLink);
                        }

                        DialogResult = true;
                    }
                }
            }
        }

        private void SaveNewTrip(string myTripId, string savedFolderLink)
        {
            var Folder = AppDomain.CurrentDomain.BaseDirectory;
            myTripId = myTripId.Replace(" ", "");

            //Main trip image link: Resources\\Images\\TRIP{}\\TRIP{}.jpg
            var tripImgLink = $"{savedFolderLink}\\{myTripId}.jpg";
            
            //Save trip Image
            if (_tripImageLink.Length > 0)
            {
                //Save Image
                var myFilePath = $"{Folder}{tripImgLink}";

                if (_tripImageLink != myFilePath)
                {
                    try
                    {
                        //Check if existed image having same name then replace
                        MyFileManager.CheckExistedFile(myFilePath);
                        //Copy image to new folder
                        System.IO.File.Copy(_tripImageLink, myFilePath);
                    }
                    catch { /*do nothing*/ }
                }
            }
            else
            {
                tripImgLink = "";
            }

            //Create Trip
            var statusIndex = tripStatusComboBox.SelectedIndex;
            var newTrip = new TRIP
            {
                TRIP_ID = myTripId,
                TRIP_NAME = tripNameTextBox.Text,
                TRIP_DESTINATION = tripDestinationTextBox.Text,
                TRIP_DESCRIPTION = tripDescriptionTextBox.Text,
                TRIP_STATUS = statusIndex,
                DATE_BEGIN = tripDateBeginDatePicker.SelectedDate,
                DATE_FINISH = tripDateFinishDatePicker.SelectedDate,
                IMAGE_LINK = tripImgLink,
                EXIST_STATUS = true
            };
            //Save New Trip
            db.TRIPs.Add(newTrip);
            db.SaveChanges();
        }

        private void SaveExpense(string myTripId)
        {
            myExpense = new List<EXPENSE>();
            int myExpenseCode = 0;

            for (int i = 0; i < myExpenseInput.Count; i++)
            {
                if (myExpenseInput[i].CostTBCheck == true && myExpenseInput[i].ExpenseTBCheck == true)
                {
                    myExpense.Add(new EXPENSE
                    {
                        TRIP_ID = myTripId,
                        EXPENSE_ID = $"E{myExpenseCode}",
                        EXPENSE_DESCRIPTION = myExpenseInput[i].ExpenseTB.Text,
                        COST = myExpenseInput[i].Cost
                    });
                    myExpenseCode++;
                }
            }

            //Save expense
            for (int i = 0; i < myExpense.Count; i++)
            {
                db.EXPENSEs.Add(myExpense[i]);
                db.SaveChanges();
            }
        }

        private void SaveVisitLocation(string myTripId, string savedFolderLink)
        {
            var Folder = AppDomain.CurrentDomain.BaseDirectory;
            myTripId = myTripId.Replace(" ", "");

            var tempVisitLocs = myVisitLoc.OrderBy(c => c.VISIT_LOC_ID).ToList();

            for (int i = 0; i < tempVisitLocs.Count; i++)
            {
                //create ID
                var visitLocId = $"VL{i}";
                //Visit Location image link: Resources\\Images\\TRIP{}\\TRIP{}_VL{}.jpg
                var visitLocImgLink = $"{savedFolderLink}\\{myTripId}_{visitLocId}.jpg";

                //Save visit location Image
                if (tempVisitLocs[i].IMAGE_LINK.Length > 0)
                {
                    //Save Image
                    var myFilePath = $"{Folder}{visitLocImgLink}";

                    if (tempVisitLocs[i].IMAGE_LINK != myFilePath)
                    {
                        try
                        {
                            //Check if existed image having same name then replace
                            MyFileManager.CheckExistedFile(myFilePath);
                            //Copy image to new folder
                            System.IO.File.Copy(tempVisitLocs[i].IMAGE_LINK, myFilePath);
                        }
                        catch { /*do nothing*/}
                    }
                }
                else
                {
                    visitLocImgLink = "";
                }

                var newVisitLoc = new VISIT_LOCATION
                {
                    TRIP_ID = myTripId,
                    VISIT_LOC_ID = visitLocId,
                    DATE_BEGIN = tempVisitLocs[i].DATE_BEGIN,
                    DATE_FINISH = tempVisitLocs[i].DATE_FINISH,
                    VISIT_LOC_DESTINATION = tempVisitLocs[i].VISIT_LOC_DESTINATION,
                    VISIT_LOC_DESCRIPTION = tempVisitLocs[i].VISIT_LOC_DESCRIPTION,
                    IMAGE_LINK = visitLocImgLink
                };

                db.VISIT_LOCATION.Add(newVisitLoc);
                db.SaveChanges();
            }

        }

        private void SaveMember(string myTripId, string savedFolderLink)
        {
            var Folder = AppDomain.CurrentDomain.BaseDirectory; 
            myTripId = myTripId.Replace(" ", "");
            var tempMember = myMember.OrderBy(c => c.MEMBER_ID).ToList();

            for (int i = 0; i < tempMember.Count; i++)
            {
                //create ID
                var memberId = $"M{i}";
                //Visit Location image link: Resources\\Images\\TRIP{}\\TRIP{}_M{}.jpg
                var avatarLink = $"{savedFolderLink}\\{myTripId}_{memberId}.jpg";

                //Save avatar
                if (tempMember[i].AVATAR.Length > 0)
                {
                    //Save Image
                    var myFilePath = $"{Folder}{avatarLink}";

                    if (tempMember[i].AVATAR != myFilePath)
                    {
                        try
                        {
                            //Check if existed image having same name then replace
                            MyFileManager.CheckExistedFile(myFilePath);
                            //Copy image to new folder
                            System.IO.File.Copy(tempMember[i].AVATAR, myFilePath);
                        }
                        catch { /*do nothing*/ }
                    }
                }
                else
                {
                    avatarLink = "";
                }

                var newMember = new MEMBER
                {
                    TRIP_ID = myTripId,
                    MEMBER_ID = memberId,
                    MEMBER_NAME = tempMember[i].MEMBER_NAME,
                    EMAIL = tempMember[i].EMAIL,
                    PHONE = tempMember[i].PHONE,
                    AVATAR = avatarLink
                };


                //save member
                db.MEMBERs.Add(newMember);
                db.SaveChanges();

                var newTripSplit = new List<TRIP_SPLIT>();
                var tempTripSplit = myTripSplit.Where(c => c.MEMBER_ID == tempMember[i].MEMBER_ID).ToList();
                //Save Member Trip Split
                for (int j = 0; j < tempTripSplit.Count; j++)
                {
                    newTripSplit.Add(new TRIP_SPLIT
                    {
                        TRIP_ID = myTripId,
                        MEMBER_ID = memberId,
                        PAYMENT_ID = $"TS{j}",
                        PAYMENT_DESCRIPTION = tempTripSplit[j].PAYMENT_DESCRIPTION,
                        PAID_COST = tempTripSplit[j].PAID_COST
                    });
                }
                for (int j = 0; j < newTripSplit.Count; j++)
                {
                    db.TRIP_SPLIT.Add(newTripSplit[j]);
                    db.SaveChanges();
                }
            }
        }

        private void tripDateBeginDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var finishDate = tripDateFinishDatePicker.SelectedDate;
            var beginDate = tripDateBeginDatePicker.SelectedDate;
            if (finishDate != null)
            {
                if (beginDate != null)
                {
                    if (beginDate > finishDate)
                    {
                        MessageBox.Show("Ngày bắt đầu sau ngày kết thúc", "Lỗi");
                        tripDateBeginDatePicker.SelectedDate = null;
                    }
                    else
                    {
                        visitLocDateBeginDatePicker.DisplayDateStart = beginDate;
                        visitLocDateFinishDatePicker.DisplayDateStart = beginDate;
                    }
                }
            }
            else
            {
                if (beginDate != null)
                {
                    visitLocDateBeginDatePicker.DisplayDateStart = beginDate;
                    visitLocDateFinishDatePicker.DisplayDateStart = beginDate;
                }
            }
        }

        private void tripDateFinishDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var finishDate = tripDateFinishDatePicker.SelectedDate;
            var beginDate = tripDateBeginDatePicker.SelectedDate;
            if (beginDate != null)
            {
                if (finishDate != null)
                {
                    if (beginDate > finishDate)
                    {
                        MessageBox.Show("Ngày kết thúc trước ngày bắt đầu", "Lỗi");
                        tripDateFinishDatePicker.SelectedDate = null;
                    }
                    else
                    {
                        visitLocDateBeginDatePicker.DisplayDateEnd = finishDate;
                        visitLocDateFinishDatePicker.DisplayDateEnd = finishDate;
                    }
                }
            }
            else
            {
                if (finishDate != null)
                {
                    visitLocDateBeginDatePicker.DisplayDateEnd = finishDate;
                    visitLocDateFinishDatePicker.DisplayDateEnd = finishDate;
                }
            }
        }

        private void visitLocDateBeginDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var beginDate = tripDateBeginDatePicker.SelectedDate;
            var finishDate = tripDateFinishDatePicker.SelectedDate;
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
            var beginDate = tripDateBeginDatePicker.SelectedDate;
            var finishDate = tripDateFinishDatePicker.SelectedDate;
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
