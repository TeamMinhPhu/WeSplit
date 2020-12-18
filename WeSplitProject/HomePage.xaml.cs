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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplitProject.Classes;
using System.Timers;

namespace WeSplitProject
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		public delegate void NewWindowOpenHandler(string ID);
		public event NewWindowOpenHandler NewWindowOpen;
	
		int _total_items;
		int _current_page = 1;
		int _itemPerPage = 12;
		int _total_page;
		int _trip_status = -1;
		int _search_type = 2;
		string _search ="";
		int _selected_index;
		WeSplitDBEntities db = new WeSplitDBEntities();
		bool selectionMode = false;
		bool editMode = false;

		private System.Timers.Timer _timer = new System.Timers.Timer(300); //time delayed to UpdatePage after resizing window

		public List<string> PageSelection { get; set; }
		public string CurrentPage { get; set; }


		BindingList<ViewModel> viewModels = new BindingList<ViewModel>();
		private BindingList<ViewModel> GetViewModel()
		{
			BindingList<ViewModel> result = new BindingList<ViewModel>();
			_itemPerPage = Paging.GetItemsPerPage(itemsView.ActualWidth, itemsView.ActualHeight);

			////////
			///Access Data base
			//var query = TripDAO.GetAll().Skip((_current_page - 1) * _itemPerPage).Take(_itemPerPage).Select(c => new { c.ID, c.Name, c.StartedDate, c.EndedDate, c.CoverImage }).ToList();
			//var unfilteredQuery = db.TRIPs.Where(c=>c.TRIP_NAME.Contains(_search) && c.EXIST_STATUS == true).Select(c => new { c.TRIP_ID, c.TRIP_NAME, c.DATE_BEGIN, c.DATE_FINISH, c.IMAGE_LINK, c.TRIP_STATUS }).ToList();
			var unfilteredQuery = getNewData();
			_total_items = unfilteredQuery.Count();
			var query = unfilteredQuery.OrderBy(c => c.TRIP_ID).Skip((_current_page - 1) * _itemPerPage).Take(_itemPerPage).Select(c => new { c.TRIP_ID, c.TRIP_NAME, c.DATE_BEGIN, c.DATE_FINISH, c.IMAGE_LINK, c.TRIP_STATUS }).ToList();
			foreach (var viewData in query)
			{
				ViewModel viewModel = new ViewModel();

				viewModel.ID = viewData.TRIP_ID;

				viewModel.Name = viewData.TRIP_NAME;
				
                if (viewData.IMAGE_LINK.Length <= 0)
                {
					viewModel.CoverImage = "Resources/Images/sora.jpg";

				}
                else
                {
					viewModel.CoverImage = viewData.IMAGE_LINK;
				}
				
				if(viewData.DATE_BEGIN != null)
                {
					viewModel.StartedDate = viewData.DATE_BEGIN?.ToString("dd/MM/yyyy");

					if (viewData.DATE_FINISH != null)
					{
						viewModel.StartedDate += " - " + viewData.DATE_FINISH?.ToString("dd/MM/yyyy");
					}

					switch (viewData.TRIP_STATUS)
					{
						case 0:
							viewModel.StartedDate += " - Lên kế hoạch";
							break;
						case 1:
							viewModel.StartedDate += " - Bắt đầu";
							break;
						case 2:
							viewModel.StartedDate += " - Đang đi";
							break;
						case 3:
							viewModel.StartedDate += " - Kết thúc";
							break;
					}
				}
                else
                {
					switch (viewData.TRIP_STATUS)
					{
						case 0:
							viewModel.StartedDate += "Lên kế hoạch";
							break;
						case 1:
							viewModel.StartedDate += "Bắt đầu";
							break;
						case 2:
							viewModel.StartedDate += "Đang đi";
							break;
						case 3:
							viewModel.StartedDate += "Kết thúc";
							break;
					}
				}

				

				result.Add(viewModel);
			}

			return result;
		}

		public HomePage()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.SizeChanged += _size_changed;
			_timer.AutoReset = false;
			_timer.Elapsed += _timer_Elapsed;

			_itemPerPage = Paging.GetItemsPerPage(itemsView.ActualWidth, itemsView.ActualHeight);
			_total_items = db.TRIPs.Where(c => c.EXIST_STATUS == true).Count();
			_total_page = Paging.GetTotalPages(_total_items, _itemPerPage);
			UpdatePage();

			AllStatus.IsChecked = true;
			searchByTripName.IsChecked = true;

			viewModels.Clear();
			viewModels = GetViewModel();
			ListViewTrips.ItemsSource = viewModels;
		}


		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			keywordPlaceholderTextBlock.Visibility = Visibility.Hidden;
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (keywordTextBox.Text.Length == 0)
			{
				keywordPlaceholderTextBlock.Visibility = Visibility.Visible;
			}
		}

		private void keywordTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			//if (e.Key == Key.Return)
			{
				_search = keywordTextBox.Text;
				RenewDataModel();
			}
		}

		private void searchButton_Click(object sender, RoutedEventArgs e)
		{
			_search = keywordTextBox.Text;
			RenewDataModel();
		}

		//Detail
		private void trip_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (selectionMode == true)
			{
				//do nothing
			}
			else
			{
				try
				{
					string ID = viewModels[_selected_index].ID;
					//TRIP tripDetail = db.TRIPs.First(c => c.TRIP_ID == ID);
					this.NavigationService.Navigate(new DetailPage(ID));
				}
				catch
				{
					//do nothing
				}
			}

		}



		private void ListViewTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				_selected_index = ListViewTrips.SelectedIndex;
			}
			catch
			{
				//do nothing
			}
		}

		#region "filter"
		private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_current_page = 1;
			UpdateView();
		}
		#endregion

		//Edit
		private void editButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var index = ListViewTrips.SelectedIndex;
			if (index >= 0) 
            {
				var myID = viewModels[index].ID;

				NewWindowOpen?.Invoke(myID);
			}

		}

		#region "Paging"

		//Delay time to update ItemsPerPage when window is resizing--> no more lags --> :)
		private void _size_changed(object sender, SizeChangedEventArgs e)
		{
			_timer.Stop();
			_timer.Start();
		}

		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(() =>
			{
				_current_page = 1;
				UpdateView();
			});
			_timer.Stop();
		}
		private void UpdateView()
		{
			viewModels.Clear();
			viewModels = GetViewModel();
			ListViewTrips.ItemsSource = viewModels;
			UpdatePage();
		}

		private void UpdatePage()
		{
			//_total_items = _trips.Where(c => c.EXIST_STATUS == true).Count();
			_total_page = Paging.GetTotalPages(_total_items, _itemPerPage);
			viewTotalPages.Text = "/ " + _total_page.ToString();
			paging.ItemsSource = PagingViewModel.UpdatePage(_total_page);
			paging.SelectedIndex = _current_page - 1;
		}

		private void previousButton_Click(object sender, RoutedEventArgs e)
		{
			if (_current_page > 1)
			{
				_current_page--;
			}
			else
			{
				_current_page = _total_page;
			}
			paging.SelectedIndex = _current_page - 1;
			//this.NavigationService.Navigate(new AboutUsPage());
		}


		private void nextButton_Click(object sender, RoutedEventArgs e)
		{
			if (_current_page < _total_page)
			{
				_current_page++;
			}
			else
			{
				_current_page = 1;
			}
			paging.SelectedIndex = _current_page - 1;
		}

		private void paging_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_current_page = paging.SelectedIndex + 1;
			UpdateView();
		}

		#endregion

		private void addButton_MouseUp(object sender, MouseButtonEventArgs e)
		{
			var addScreen = new CreateNewTrip();
			//addScreen.ShowDialog();
			if (addScreen.ShowDialog() == true)
			{
				_current_page = 1;
				paging.SelectedIndex = 0;
				UpdateView();
			}
		}
		
		private void infoButton_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.NavigationService.Navigate(new AboutUsPage());

		}

        private void exitButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
			var index = ListViewTrips.SelectedIndex;
			if (index >= 0)
			{
                try
                {
					var myID = viewModels[index].ID;
					var trip = db.TRIPs.Where(c => c.TRIP_ID == myID).FirstOrDefault();
                    if (trip != null)
                    {
						trip.EXIST_STATUS = false;
						db.SaveChanges();

						_current_page = 1;
						paging.SelectedIndex = 0;
						UpdateView();

						MessageBox.Show("Đã xóa chuyến đi");
					}
                    else
                    {
						MessageBox.Show("Không tìm thấy chuyến đi");
					}
				}
                catch 
				{ 
					MessageBox.Show("Không thể xóa chuyến đi", "Lỗi"); 
				}
			}
		}


		private List<TRIP> getNewData()
		{
			List<TRIP> result = new List<TRIP>();
			db.Dispose(); //prevent memory leak..

			///filter
			db = new WeSplitDBEntities();
			if (_trip_status == -1)
			{
				result = db.TRIPs.Where(c =>c.EXIST_STATUS == true).ToList();
			}
			else
			{
				result = db.TRIPs.Where(c => c.EXIST_STATUS == true && c.TRIP_STATUS == _trip_status).ToList();
			}

			///search
			if (_search_type == 0)
			{
				result = result.Where(c => c.TRIP_DESTINATION.Contains(_search)).ToList();
			}
			else if (_search_type == 1)
			{
				var memberList = db.MEMBERs.Where(c => c.MEMBER_NAME.Contains(_search));
				List<string> Id = new List<string>();
				foreach (var member in memberList)
				{
					if (Id.Contains(member.TRIP_ID))
					{
						// do nothing
					}
					else
					{
						Id.Add(member.TRIP_ID);
					}
				}
				result = result.Where(c => c.EXIST_STATUS == true && Id.Contains(c.TRIP_ID)).ToList();
			}
			else
			{
				result = result.Where(c => c.TRIP_NAME.Contains(_search)).ToList();
			}
			return result;
		}

		private void All_Checked(object sender, RoutedEventArgs e)
		{
			_trip_status = -1;
			RenewDataModel();
		}

		private void Plan_Checked(object sender, RoutedEventArgs e)
		{
			_trip_status = 0;
			RenewDataModel();
		}

		private void Begin_Checked(object sender, RoutedEventArgs e)
		{
			_trip_status = 1;
			RenewDataModel();
		}

		private void OnGoing_Checked(object sender, RoutedEventArgs e)
		{
			_trip_status = 2;
			RenewDataModel();
		}

		private void Done_Checked(object sender, RoutedEventArgs e)
		{
			_trip_status = 3;
			RenewDataModel();
		}

		private void RenewDataModel()
		{
			_current_page = 1;
			UpdateView();
		}

		private void searchByPlaces_Checked(object sender, RoutedEventArgs e)
		{
			_search_type = 0;
			UpdateView();
		}

		private void searchByMembers_Checked(object sender, RoutedEventArgs e)
		{
			_search_type = 1;
			UpdateView();
		}

		private void searchByTripName_Checked(object sender, RoutedEventArgs e)
		{
			_search_type = 2;
			UpdateView();
		}

		private void settingsButton_MouseUp(object sender, MouseButtonEventArgs e)
		{
			var settings = new SettingScreen();
			settings.ShowDialog();
		}
	}
}
