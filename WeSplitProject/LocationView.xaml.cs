using Microsoft.Win32;
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
    /// Interaction logic for LocationView.xaml
    /// </summary>
    public partial class LocationView : Window
    {
        VISIT_LOCATION myVisitLoc;
        WeSplitDBEntities db = new WeSplitDBEntities();

        public LocationView(VISIT_LOCATION tempVisitLoc)
        {
            InitializeComponent();
            myVisitLoc = tempVisitLoc;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            visitLocDestinationTextBlock.Text = myVisitLoc.VISIT_LOC_DESTINATION;

            if(myVisitLoc.DATE_BEGIN != null)
            {
                visitLocDateBeginTextBlock.Text = $"{myVisitLoc.DATE_BEGIN}";
            }
            else
            {
                visitLocDateBeginTextBlock.Text = "Chưa biết";
            }

            if (myVisitLoc.DATE_FINISH != null)
            {
                visitLocDateFinishTextBlock.Text = $"{myVisitLoc.DATE_FINISH}";
            }
            else
            {
                visitLocDateFinishTextBlock.Text = "Chưa biết";
            }

            visitLocDescriptionTextBlock.Text = myVisitLoc.VISIT_LOC_DESCRIPTION;

            if (myVisitLoc.IMAGE_LINK.Length > 0)
            {
                try
                {
                    var Folder = AppDomain.CurrentDomain.BaseDirectory;
                    var Bitmap = new BitmapImage(new Uri($"{Folder}{myVisitLoc.IMAGE_LINK}", UriKind.Absolute));
                    visitLocImage.Source = Bitmap;
                }
                catch { /*do nothing*/ }
            }
        }
    }
}
