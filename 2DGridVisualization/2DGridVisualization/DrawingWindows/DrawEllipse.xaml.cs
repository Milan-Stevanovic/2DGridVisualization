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
    public partial class DrawEllipse : Window
    {
        public DrawEllipse()
        {
            InitializeComponent();

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbStroke.ItemsSource = colors;
            cmbFill.ItemsSource = colors;
            cmbEllipseTextColor.ItemsSource = colors;
        }

        private void btnDrawEllipse_Click(object sender, RoutedEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            TextBlock ellipseText = new TextBlock();

            double width;
            double height;
            double strokeThickness;
            double opacity;

            Double.TryParse(txtRadiusX.Text, out width);
            Double.TryParse(txtRadiusY.Text, out height);
            Double.TryParse(txtStrokeThickness.Text, out strokeThickness);
            Double.TryParse(txtOpacity.Text, out opacity);

            if (width < 0)
                width = 0;
            if (height < 0)
                height = 0;
            if (strokeThickness < 0)
                strokeThickness = 1;

            if (opacity < 0)
                opacity = 0;
            else
                opacity = opacity / 100;

            if (opacity > 100)
                opacity = 1;

            SolidColorBrush strokeColor = Brushes.White;
            SolidColorBrush fillColor = Brushes.Transparent;
            SolidColorBrush ellipseTextColor = Brushes.White;


            if (cmbStroke.SelectedItem != null)
                strokeColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbStroke.SelectedItem.ToString());

            if (cmbFill.SelectedItem != null)
                fillColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbFill.SelectedItem.ToString());

            if (cmbEllipseTextColor.SelectedItem != null)
                ellipseTextColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbEllipseTextColor.SelectedItem.ToString());

            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.Stroke = strokeColor;
            ellipse.Fill = fillColor;
            ellipse.StrokeThickness = strokeThickness;
            ellipseText.Foreground = ellipseTextColor;
            ellipseText.Text = txtEllipseText.Text;
            ellipseText.FontSize = 14;
            ellipse.Opacity = opacity;


            MainWindow.ellipse = ellipse;
            MainWindow.ellipseText = ellipseText;

            this.Close();
        }
    }
}
