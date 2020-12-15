using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeSplitProject
{
	/// <summary>
	/// Interaction logic for DetailPage.xaml
	/// </summary>
	public partial class DetailPage : Page
	{
		TRIP _trip;
		public DetailPage(TRIP tripDetail)
		{
			InitializeComponent();
			_trip = tripDetail;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			members.ItemsSource = _trip.MEMBERs;
		}

		private void DataGridTextColumn_MouseUp(object sender, MouseButtonEventArgs e)
		{

		}

		private void members_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MEMBER m = (MEMBER)members.SelectedItem;
			MessageBox.Show(m.ToString());
		}

		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show("a");
		}

		private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			var cellInfo = members.SelectedCells[1];
			var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

			MEMBER myMember = _trip.MEMBERs.First(c => c.MEMBER_ID == content);
			var x = myMember.TRIP_SPLIT;
			foreach(var a in x)
			{
				MessageBox.Show(a.MEMBER.ToString() + ":" + a.PAID_COST.ToString());
			}

		}
	}
}
