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
    public partial class EditPolygon : Window
    {
        public static Polygon editPolygon;
        public static TextBlock editPolygonText;


        public EditPolygon(Canvas polygonCanvas)
        {
            InitializeComponent();

            editPolygon = (Polygon)polygonCanvas.Children[0];
            editPolygonText = (TextBlock)polygonCanvas.Children[1];

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbStroke.ItemsSource = colors;
            cmbFill.ItemsSource = colors;
            cmbPolygonTextColor.ItemsSource = colors;


            foreach (var color in colors)
            {
                System.Drawing.Color fillColor = System.Drawing.ColorTranslator.FromHtml(editPolygon.Fill.ToString());
                System.Drawing.Color strokeColor = System.Drawing.ColorTranslator.FromHtml(editPolygon.Stroke.ToString());
                System.Drawing.Color polygonTextColor = System.Drawing.ColorTranslator.FromHtml(editPolygonText.Foreground.ToString());

                if (color.Equals(fillColor.Name))
                    cmbFill.SelectedItem = color;
                if (color.Equals(strokeColor.Name))
                    cmbStroke.SelectedItem = color;
                if (color.Equals(polygonTextColor.Name))
                    cmbPolygonTextColor.SelectedItem = color;
            }

            txtStrokeThickness.Text = editPolygon.StrokeThickness.ToString();
            txtOpacity.Text = (editPolygon.Opacity * 100).ToString();
        }

        private void btnEditPolygon_Click(object sender, RoutedEventArgs e)
        {
            double strokeThickness;
            double opacity;


            Double.TryParse(txtStrokeThickness.Text, out strokeThickness);
            Double.TryParse(txtOpacity.Text, out opacity);

            if (strokeThickness < 0)
                strokeThickness = 0;

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

            editPolygon.Stroke = strokeColor;
            editPolygon.Fill = fillColor;
            editPolygon.StrokeThickness = strokeThickness;
            editPolygonText.Foreground = polygonTextColor;
            editPolygon.Opacity = opacity;
            this.Close();
        }
    }
}
