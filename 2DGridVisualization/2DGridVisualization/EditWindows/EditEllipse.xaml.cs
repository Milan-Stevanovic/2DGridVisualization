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
    /// <summary>
    /// Interaction logic for EditEllipse.xaml
    /// </summary>
    public partial class EditEllipse : Window
    {
        public static Ellipse editEllipse;
        public static TextBlock editEllipseText;

        public EditEllipse(Canvas ellipseCanvas)
        {
            InitializeComponent();

            editEllipse = (Ellipse)ellipseCanvas.Children[0];
            editEllipseText = (TextBlock)ellipseCanvas.Children[1];

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbStroke.ItemsSource = colors;
            cmbFill.ItemsSource = colors;
            cmbEllipseTextColor.ItemsSource = colors;

            foreach (var color in colors)
            {
                System.Drawing.Color fillColor = System.Drawing.ColorTranslator.FromHtml(editEllipse.Fill.ToString());
                System.Drawing.Color strokeColor = System.Drawing.ColorTranslator.FromHtml(editEllipse.Stroke.ToString());
                System.Drawing.Color ellipseTextColor = System.Drawing.ColorTranslator.FromHtml(editEllipseText.Foreground.ToString());

                if (color.Equals(fillColor.Name))
                    cmbFill.SelectedItem = color;
                if (color.Equals(strokeColor.Name))
                    cmbStroke.SelectedItem = color;
                if (color.Equals(ellipseTextColor.Name))
                    cmbEllipseTextColor.SelectedItem = color;
            }

            txtStrokeThickness.Text = editEllipse.StrokeThickness.ToString();
            txtOpacity.Text = (editEllipse.Opacity * 100).ToString();
        }

        private void btnEditEllipse_Click(object sender, RoutedEventArgs e)
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
            SolidColorBrush ellipseTextColor = Brushes.White;


            if (cmbStroke.SelectedItem != null)
                strokeColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbStroke.SelectedItem.ToString());

            if (cmbFill.SelectedItem != null)
                fillColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbFill.SelectedItem.ToString());

            if (cmbEllipseTextColor.SelectedItem != null)
                ellipseTextColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbEllipseTextColor.SelectedItem.ToString());

            editEllipse.Stroke = strokeColor;
            editEllipse.Fill = fillColor;
            editEllipse.StrokeThickness = strokeThickness;
            editEllipse.Opacity = opacity;
            editEllipseText.Foreground = ellipseTextColor;
            this.Close();
        }
    }
}
