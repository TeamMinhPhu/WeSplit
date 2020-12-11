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
            public string Trip_Id { get; set; }
            public string Trip_Destination { get; set; }
        }

        class ExpenseInput
        {
            public TextBox ExpenseTB { get; set; }
            public TextBox CostTB { get; set; }
            public bool ExpenseTBCheck { get; set; }
            public bool CostTBCheck { get; set; }
            public int Cost { get; set; }
        }

        string _tripImageLink;
        string _visitLocImageLink;
        string _avatarImageLink;

        TRIP myTrip;
        BindingList<EXPENSE> myExpense;
        BindingList<VISIT_LOCATION> myVisitLoc;
        BindingList<TRIP_SPLIT> myTripSplit;
        BindingList<MEMBER> myMember;
        BindingList<TripDestination> myTripDes;
        List<ExpenseInput> myExpenseInput;

        long expenseTotalCost;

        WeSplitDBEntities db = new WeSplitDBEntities();
        
        

        public CreateNewTrip()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            expenseTotalCost = 0;

            var temp = db.TRIPs.Select(c => new { c.TRIP_ID, c.TRIP_DESTINATION }).ToList();
            myTripDes = new BindingList<TripDestination>();
            foreach(var item in temp)
            {
                myTripDes.Add(new TripDestination { Trip_Id = item.TRIP_ID, Trip_Destination = item.TRIP_DESTINATION });
            }
            tripDestinationComboBox.ItemsSource = myTripDes;

            myExpenseInput = new List<ExpenseInput>();
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
                if (IsImageFile(_tripImageLink))
                {
                    var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
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
                    if (IsImageFile(_tripImageLink))
                    {
                        var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
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
                if (IsImageFile(_tripImageLink))
                {
                    var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
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
                    if (IsImageFile(_tripImageLink))
                    {
                        var Bitmap = new BitmapImage(new Uri(_tripImageLink, UriKind.Absolute));
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

            myExpenseInput.Add(new ExpenseInput { ExpenseTB = expenseDesTextBox, CostTB = CostTextBox, CostTBCheck = false, ExpenseTBCheck = false, Cost = 0});

            SPExpense.Children.Add(subExpenseSP);
        }

        private void addVisitLocBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteVisitLocBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addMemberBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteMemberBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addPaymentLocBtn_Click(object sender, RoutedEventArgs e)
        {

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
                if(textbox.Text.Length <= 0)
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
                        expenseTotalCost += myExpenseInput[flag].Cost;
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

            bool checkInt = false;
            int result = 0;

            if (textbox.Text.Length > 0)
            {
                checkInt = Int32.TryParse(textbox.Text, out result);
            }
            

            if (myExpenseInput[flag].CostTBCheck) 
            {
                if(!checkInt)
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
                        expenseTotalCost = expenseTotalCost - myExpenseInput[flag].Cost + result;
                    }
                    myExpenseInput[flag].CostTBCheck = true;
                    myExpenseInput[flag].Cost = result;
                }
            }
            else
            {
                if(checkInt)
                {
                    if (myExpenseInput[flag].ExpenseTBCheck)
                    {
                        expenseTotalCost = expenseTotalCost - myExpenseInput[flag].Cost + result;                        
                    }
                    myExpenseInput[flag].CostTBCheck = true;
                    myExpenseInput[flag].Cost = result;
                }
            }

            totalExpenseTextBlock.Text = $"Tổng chi phí: {expenseTotalCost.ToString()}";
        }
    }




}
