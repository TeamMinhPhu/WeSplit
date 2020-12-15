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
		
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MouseDown += Window_MouseDown;
			MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
			MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			content.Navigate(new HomePage());
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

		
	}
}
