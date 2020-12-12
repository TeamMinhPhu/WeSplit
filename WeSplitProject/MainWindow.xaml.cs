using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WeSplitProject.Classes;

namespace WeSplitProject
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		int _total_items = 19;
		int _current_page = 1;
		int _itemPerPage = 12;
		int _total_page;
		string _search;
		int _selected_index;

		private System.Timers.Timer _timer = new System.Timers.Timer(300); //time delayed to UpdatePage after resizing window

		public List<string> PageSelection { get; set; }
		public string CurrentPage { get; set; }


		BindingList<ViewModel> viewModels = new BindingList<ViewModel>();
		private BindingList<ViewModel> GetViewModel()
		{
			BindingList<ViewModel> result = new BindingList<ViewModel>();
			_itemPerPage = Paging.GetItemsPerPage(itemsView.ActualWidth, itemsView.ActualHeight);
			var query = TripDAO.GetAll().Skip((_current_page - 1) * _itemPerPage).Take(_itemPerPage).Select(c => new { c.ID, c.Name, c.StartedDate, c.EndedDate, c.CoverImage }).ToList();
			foreach (var viewData in query)
			{
				ViewModel viewModel = new ViewModel();

				viewModel.ID = viewData.ID;

				viewModel.Name = viewData.Name + viewData.ID;
				viewModel.CoverImage = viewData.CoverImage;
				viewModel.StartedDate = viewData.StartedDate.ToString("dd/MM/yyyy");
				if (viewData.EndedDate != DateTime.MinValue)
				{
					viewModel.StartedDate += " - " + viewData.EndedDate.ToString("yyyy/MM/dd");
				}
				else
				{
					viewModel.StartedDate += " (Đang đi)";
				}
				result.Add(viewModel);
			}

			return result;
		}

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MouseDown += Window_MouseDown;
			MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
			MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			
			this.SizeChanged += _size_changed;
			_timer.AutoReset = false;
			_timer.Elapsed += _timer_Elapsed;

			_itemPerPage = Paging.GetItemsPerPage(itemsView.ActualWidth, itemsView.ActualHeight);
			_total_page = Paging.GetTotalPages(_total_items, _itemPerPage);
			UpdatePage();

			viewModels = GetViewModel();
			ListViewTrips.ItemsSource = viewModels;
			filter.SelectedIndex = 0;
		}

		//drag window
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				DragMove();
		}
		#region "Title Bar Buttons"
		//close
		private void closeProgramButton_Click(object sender, RoutedEventArgs e)
		{
			//saveConfig();
			Application.Current.Shutdown();
		}
		//maximize - unmaximize
		private void maximizeProgramButton_Click(object sender, RoutedEventArgs e)
		{
			if (WindowState != WindowState.Maximized)
			{
				WindowState = WindowState.Maximized;
				var Bitmap = new BitmapImage(new Uri("Resources/Icons/unmaximize.png", UriKind.Relative));
				maximizeButtonImage.Source = Bitmap;
			}
			else
			{
				WindowState = WindowState.Normal;
				var Bitmap = new BitmapImage(new Uri("Resources/Icons/maximize.png", UriKind.Relative));
				maximizeButtonImage.Source = Bitmap;
			}
		}
		//minimize
		private void minimizeProgramButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}
		#endregion

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

		private void trip_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show("Detail trip: " + viewModels[_selected_index].ID.ToString());
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

		private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_current_page = 1;
			UpdateView();
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
			_total_page = Paging.GetTotalPages(_total_items, _itemPerPage);
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
	}
}
