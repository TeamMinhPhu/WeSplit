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

		int _current_page = 1;
		string _search;
		int _selected_index;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MouseDown += Window_MouseDown;
			MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
			MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			ListViewTrips.ItemsSource = TripDAO.GetAll();
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
			MessageBox.Show("Detail trip: " + _selected_index.ToString());
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

		private void UpdateView()
		{

		}

		private void UpdatePage()
		{

		}
	}
}
