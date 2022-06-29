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
    public partial class DrawPolygon : Window
    {
        public DrawPolygon()
        {
            InitializeComponent();

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbFill.ItemsSource = colors;
            cmbPolygonTextColor.ItemsSource = colors;
            cmbStroke.ItemsSource = colors;
        }

        private void btnDrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            Polygon polygon = new Polygon();
            TextBlock polygonText = new TextBlock();

            double strokeThickness;
            double opacity;

            Double.TryParse(txtStrokeThickness.Text, out strokeThickness);
            Double.TryParse(txtOpacity.Text, out opacity);

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
            SolidColorBrush polygonTextColor = Brushes.White;


            if (cmbStroke.SelectedItem != null)
                strokeColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbStroke.SelectedItem.ToString());

            if (cmbFill.SelectedItem != null)
                fillColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbFill.SelectedItem.ToString());

            if (cmbPolygonTextColor.SelectedItem != null)
                polygonTextColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbPolygonTextColor.SelectedItem.ToString());

            polygon.StrokeThickness = strokeThickness;
            polygon.Stroke = strokeColor;
            polygon.Fill = fillColor;
            polygon.Opacity = opacity;

            polygonText.Text = txtPolygonText.Text;
            polygonText.Foreground = polygonTextColor;


            MainWindow.polygon = polygon;
            MainWindow.polygonText = polygonText;

            this.Close();
        }
    }
}
