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
    /// Interaction logic for MemberView.xaml
    /// </summary>
    public partial class MemberView : Window
    {
        string myMemberId;
        string myTripId;
        WeSplitDBEntities db = new WeSplitDBEntities();
        BindingList<TRIP_SPLIT> myTripSplit;

        public MemberView(string tempTripId, string tempMemberId)
        {
            InitializeComponent();
            myMemberId = tempMemberId;
            myTripId = tempTripId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MEMBER _member = new MEMBER();
            List<TRIP_SPLIT> _tripsplitList = new List<TRIP_SPLIT>();
            List<EXPENSE> _expense = new List<EXPENSE>();
            long paidmoney = 0;
            long totalexpense = 0;
            long refund = 0;

            try
            {
                _member = db.MEMBERs.Where(c => c.TRIP_ID == myTripId && c.MEMBER_ID == myMemberId).FirstOrDefault();
                _tripsplitList = db.TRIP_SPLIT.Where(c => c.TRIP_ID == myTripId && c.MEMBER_ID == myMemberId).ToList();
                _expense = db.EXPENSEs.Where(c => c.TRIP_ID == myTripId).ToList();
            }
            catch
            {
                MessageBox.Show("Không tìm thấy thông tin thành viên", "Lỗi");
                this.Close();
            }

            if (_member != null)
            {
                memberNameTextBlock.Text = _member.MEMBER_NAME;
                memberPhoneTextBlock.Text = _member.PHONE;
                memberEmailTextBlock.Text = _member.EMAIL;
                paidmoney = (long)_member.PAID_MONEY;
                paidCostTextBlock.Text = $"{paidmoney}";

                if (_member.AVATAR.Length > 0)
                {
                    try
                    {
                        var Folder = AppDomain.CurrentDomain.BaseDirectory;
                        var Bitmap = new BitmapImage(new Uri($"{Folder}{_member.AVATAR}", UriKind.Absolute));
                        avatarImage.Source = Bitmap;
                    }
                    catch { /*do nothing*/ }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin thành viên", "Lỗi");
                this.Close();
            }

            var myTripSplit = new BindingList<TRIP_SPLIT>();

            if (_expense != null)
            {
                foreach(var expense in _expense)
                {
                    myTripSplit.Add(new TRIP_SPLIT { PAYMENT_DESCRIPTION = expense.EXPENSE_DESCRIPTION, PAID_COST = (long)expense.COST });
                }
            }

            if (_tripsplitList != null)
            {
                foreach (var expense in _tripsplitList)
                {
                    myTripSplit.Add(new TRIP_SPLIT { PAYMENT_DESCRIPTION = expense.PAYMENT_DESCRIPTION, PAID_COST = (long)expense.PAID_COST });
                }
            }

            if (myTripSplit.Count > 0)
            {
                for (int i = 0; i < myTripSplit.Count; i++)
                {
                    totalexpense += (long)myTripSplit[i].PAID_COST;
                }

                expenseSPHint.Visibility = Visibility.Visible;
                expenseListBox.ItemsSource = myTripSplit;
            }

            totalTextBlock.Text = $"{Convert.ToInt64(totalexpense)}";

            refund = paidmoney - totalexpense;
            refundTextBlock.Text = $"{Convert.ToInt64(refund)}";
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
