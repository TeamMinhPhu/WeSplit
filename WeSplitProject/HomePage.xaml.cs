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
		int _total_items;
		int _current_page = 1;
		int _itemPerPage = 12;
		int _total_page;
		string _search;
		int _selected_index;
		WeSplitDBEntities db = new WeSplitDBEntities();
		bool selectionMode = false;

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
			var query = db.TRIPs.OrderBy(c => c.TRIP_ID).Skip((_current_page - 1) * _itemPerPage).Take(_itemPerPage).Select(c => new { c.TRIP_ID, c.TRIP_NAME, c.DATE_BEGIN, c.DATE_FINISH, c.IMAGE_LINK }).ToList();

			foreach (var viewData in query)
			{
				ViewModel viewModel = new ViewModel();

				viewModel.ID = viewData.TRIP_ID;

				viewModel.Name = viewData.TRIP_NAME;
				viewModel.CoverImage = viewData.IMAGE_LINK;
				viewModel.StartedDate = viewData.DATE_BEGIN?.ToString("dd/MM/yyyy");
				if (viewData.DATE_FINISH != null)
				{
					viewModel.StartedDate += " - " + viewData.DATE_FINISH?.ToString("yyyy/MM/dd");
				}
				else
				{
					viewModel.StartedDate += " (Đang đi)";
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
			_total_items = db.TRIPs.Count();
			_total_page = Paging.GetTotalPages(_total_items, _itemPerPage);
			UpdatePage();

			viewModels = GetViewModel();
			ListViewTrips.ItemsSource = viewModels;
			filter.SelectedIndex = 0;
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
			if (e.Key == Key.Return)
			{
				_current_page = 1;
				_search = keywordTextBox.Text;
				UpdateView();
				UpdatePage();
			}
		}

		private void searchButton_Click(object sender, RoutedEventArgs e)
		{
			_current_page = 1;
			_search = keywordTextBox.Text;
			UpdateView();
			UpdatePage();
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
					TRIP tripDetail = db.TRIPs.First(c => c.TRIP_ID == ID);
					this.NavigationService.Navigate(new DetailPage(tripDetail));
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
				var addScreen = new CreateNewTrip(viewModels[index].ID);

				if (addScreen.ShowDialog() == true)
				{
					_current_page = 1;
					paging.SelectedIndex = 0;
					UpdatePage();
					UpdateView();
				}
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
				UpdatePage();
			});
			_timer.Stop();
		}
		private void UpdateView()
		{
			viewModels = GetViewModel();
			ListViewTrips.ItemsSource = viewModels;
		}

		private void UpdatePage()
		{
			_total_items = db.TRIPs.Count();
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
			UpdatePage();
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
				UpdatePage();
				UpdateView();
			}
		}
		
		private void infoButton_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.NavigationService.Navigate(new AboutUsPage());

		}
	}
}
