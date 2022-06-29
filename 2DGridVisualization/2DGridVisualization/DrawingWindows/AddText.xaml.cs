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

namespace _2DGridVisualization.DrawingWindows
{
    public partial class AddText : Window
    {
        public AddText()
        {
            InitializeComponent();

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbTextColor.ItemsSource = colors;
        }

        private void btnAddText_Click(object sender, RoutedEventArgs e)
        {
            Label text = new Label();

            double fontSize;

            Double.TryParse(txtFontSize.Text, out fontSize);

            if (fontSize <= 0)
                fontSize = 14;

            SolidColorBrush textColor = Brushes.White;


            if (cmbTextColor.SelectedItem != null)
                textColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbTextColor.SelectedItem.ToString());

            text.Content = txtText.Text;
            text.Foreground = textColor;
            text.FontSize = fontSize;

            MainWindow.text = text;

            this.Close();
        }
    }
}
