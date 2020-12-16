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
using LiveCharts;
using LiveCharts.Wpf;

namespace WeSplitProject
{
	/// <summary>
	/// Interaction logic for DetailPage.xaml
	/// </summary>
	public partial class DetailPage : Page
	{
		public class MySeries { 
			public string Title { get; set; }
			public double Values { get; set; }
		
		}

		public SeriesCollection Series1 { get; set; } = new SeriesCollection();
		//{
		//		new PieSeries
		//		{
		//			Title = "asd",
		//			Values = new ChartValues<double> { 15}
		//		},
		//				new PieSeries
		//		{
		//			Title = "sd",
		//			Values = new ChartValues<double> { 15}
		//		}
		//};

		class MEMVER_VIEW
		{
			public string MEMBER_NAME { get; set; }
			public string MEMBER_ID { get; set; }
			public string PHONE { get; set; }
			public string EMAIL { get; set; }
			public double EXPEND { get; set; }
			public double EXPEND_TOTAL { get; set; }
			public ICollection<TRIP_SPLIT> expends { get; set; }
			public void setExpend()
			{
				double result = 0;
				foreach (var cost in expends)
				{
					result = result + (double)cost.PAID_COST;
				}
				EXPEND = result;
			}
		}
		TRIP _trip;
		public string TripName { get; set; }
		public string Description { get; set; } = "Mô tả: ";
		public string Destination { get; set; } = "Địa điểm: ";
		public string Status { get; set; } = "Trạng thái: ";
		public string DateBegin { get; set; } = "Khởi hành: ";

		public string DateFinish { get; set; } = "Ngày về: ";
		public DetailPage(TRIP tripDetail)
		{
			InitializeComponent();
			_trip = tripDetail;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var query = _trip.MEMBERs.Select(c => new MEMVER_VIEW { EMAIL = c.EMAIL, MEMBER_NAME = c.MEMBER_NAME, MEMBER_ID = c.MEMBER_ID, PHONE = c.PHONE, expends = c.TRIP_SPLIT}).ToList();
			foreach (var item in query)
			{
				item.setExpend();
				item.EXPEND_TOTAL = item.EXPEND;
				foreach (var cost in _trip.EXPENSEs)
				{
					item.EXPEND_TOTAL += (double)cost.COST;
				}
				
				var newPie = new PieSeries
				{
					Title = item.MEMBER_NAME,
					Values = new ChartValues<double> { item.EXPEND_TOTAL }
				};
				Series1.Add(newPie);
			}
			
			members.ItemsSource = query;
			if (_trip.DATE_BEGIN == null)
			{
				DateBegin += "Chưa cập nhật";
			}
			else
			{
				DateBegin += _trip.DATE_BEGIN?.ToString("dd/MM/yyyy");
			}

			if (_trip.DATE_FINISH == null)
			{
				DateFinish += "Chưa cập nhật";
			}
			else
			{
				DateFinish += _trip.DATE_FINISH?.ToString("dd/MM/yyyy");
			}
			TripName = _trip.TRIP_NAME;
			Description += _trip.TRIP_DESTINATION;
			Destination += _trip.TRIP_DESTINATION;
			switch (_trip.TRIP_STATUS)
			{
				case 0:
					Status += "Lên kế hoạch";
					break;
				case 1:
					Status += "Bắt đầu đi";
					break;
				case 2:
					Status += "Đang đi";
					break;
				case 3:
					Status += "Đã kết thúc";
					break;
				default:
					Status += "Không xác định";
					break;

			}
			DataContext = this;
		}

		private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			var cellInfo = members.SelectedCells[1];
			var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

			MEMBER myMember = _trip.MEMBERs.First(c => c.MEMBER_ID == content);
			var x = myMember.TRIP_SPLIT;
			foreach(var a in x)
			{
				//MessageBox.Show(a.MEMBER.ToString() + ":" + a.PAID_COST.ToString());
			}

		}
	}
}
