using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using WeSplitProject.Classes;

namespace WeSplitProject
{
	/// <summary>
	/// Interaction logic for DetailPage.xaml
	/// </summary>
	public partial class DetailPage : Page
	{
		public SeriesCollection IndividualCostCollection { get; set; } = new SeriesCollection();
		public SeriesCollection TotalCostCollection { get; set; } = new SeriesCollection();
		TRIP _trip;
		public string TripName { get; set; }
		public string Description { get; set; } = "Mô tả: ";
		public string Destination { get; set; } = "Địa điểm: ";
		public string Status { get; set; } = "Trạng thái: ";
		public string DateBegin { get; set; } = "Khởi hành: ";
		public string DateFinish { get; set; } = "Ngày về: ";
		public string ImageLink { get; set; }
		public int MemberCount { get; set; }

		class MEMVER_VIEW
		{
			public string MemberName { get; set; }
			public string MemberId { get; set; }
			public string Phone { get; set; }
			public string Email { get; set; }
			public int Expend { get; set; }
			public int ExpendTotal { get; set; }
			public int Paid { get; set; }
			public string Charge { get; set; }
			public ICollection<TRIP_SPLIT> expends { get; set; }
			public void setExpend()
			{
				double result = 0;
				foreach (var cost in expends)
				{
					result = result + (double)cost.PAID_COST;
				}
				Expend = (int)result;
			}
		}

		public DetailPage(string ID)
		{
			InitializeComponent();

			WeSplitDBEntities db = new WeSplitDBEntities();
			TRIP tripDetail = db.TRIPs.First(c => c.TRIP_ID == ID);
			_trip = tripDetail;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			MemberCount = _trip.MEMBERs.Count();
			double totalIndividualCost = 0;

			////////////
			/// Member view
			var query = _trip.MEMBERs.Select(c => new MEMVER_VIEW { Email = c.EMAIL, MemberName = c.MEMBER_NAME, MemberId = c.MEMBER_ID, Phone = c.PHONE, expends = c.TRIP_SPLIT, Paid = (int)c.PAID_MONEY }).ToList();
			foreach (var member in query)
			{
				member.setExpend();
				member.ExpendTotal = member.Expend;
				foreach (var cost in _trip.EXPENSEs)
				{
					member.ExpendTotal += ((int)cost.COST / 3);
				}
				int charge = (int)(member.ExpendTotal - member.Paid);
				if (charge > 0)
				{
					member.Charge = "Nợ " + charge.ToString();
				}
				else
				{
					member.Charge = "Dư " + (-charge).ToString();
				}

				var newPie = new PieSeries
				{
					Title = member.MemberName,
					Values = new ChartValues<double> { member.ExpendTotal }, //pie chart (individual cost)
					DataLabels = true,
				};


				totalIndividualCost += member.Expend;
				IndividualCostCollection.Add(newPie);
			}
			/// End member view
			///////////

			///////////
			/// Total cost pie chart
			foreach (var item in _trip.EXPENSEs)
			{
				var newPie = new PieSeries
				{
					Title = item.EXPENSE_DESCRIPTION,
					Values = new ChartValues<double> { (double)item.COST },
					DataLabels = true,
				};
				TotalCostCollection.Add(newPie);
			}
			TotalCostCollection.Add(
					new PieSeries
					{
						Title = "Chi tiêu cá nhân",
						Values = new ChartValues<double> { totalIndividualCost },
						DataLabels = true,
					}
				);

			/// End total pie chart
			////////

			////////
			/// member table
			members.ItemsSource = query;

			////////
			/// Basic info
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

			ImageLink = _trip.IMAGE_LINK;
			//coverImage.Source = BitmapFromUri(_trip.IMAGE_LINK);
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
			/// End basic info
			//////////

			// Binding list destination
			listDestination.ItemsSource = _trip.VISIT_LOCATION;
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

		private void backButton_click(object sender, RoutedEventArgs e)
		{
			//unload image
			coverImage.Source = null;
			listDestination.ItemsSource = null;
			UpdateLayout();
			this.NavigationService.GoBack();
		}

		//public ImageSource BitmapFromUri(string source)
		//{
		//	var myFolder = AppDomain.CurrentDomain.BaseDirectory;
		//	var imageFolder = $"{myFolder}{source}";
		//	MessageBox.Show(imageFolder);
		//	Uri myUri = new Uri(imageFolder, UriKind.Absolute);
		//	var bitmap = new BitmapImage();
		//	bitmap.BeginInit();
		//	bitmap.UriSource = myUri;
		//	bitmap.CacheOption = BitmapCacheOption.OnLoad;
		//	bitmap.EndInit();
		//	bitmap.Freeze();
		//	return bitmap;
		//}
	}
}
