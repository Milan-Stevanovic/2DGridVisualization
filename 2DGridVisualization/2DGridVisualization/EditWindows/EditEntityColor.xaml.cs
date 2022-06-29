using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace _2DGridVisualization.EditWindows
{
    public partial class EditEntityColor : Window
    {
        public EditEntityColor()
        {
            InitializeComponent();

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbColor.ItemsSource = colors;
        }

        private void btnEditColor_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush color = Brushes.White;

            if (cmbColor.SelectedItem != null)
            {
                color = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbColor.SelectedItem.ToString());
            }
            else
            {
                color = Brushes.LimeGreen;
            }

            MainWindow.entityColor = color;
            this.Close();
        }
    }
}
