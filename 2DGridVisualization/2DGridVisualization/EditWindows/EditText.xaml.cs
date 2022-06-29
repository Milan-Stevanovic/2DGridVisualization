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
    public partial class EditText : Window
    {
        public static Label editLabel;

        public EditText(Label label)
        {
            InitializeComponent();

            editLabel = label;

            var colors = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(x => x.Name);

            cmbTextColor.ItemsSource = colors;


            foreach (var color in colors)
            {
                System.Drawing.Color textColor = System.Drawing.ColorTranslator.FromHtml(editLabel.Foreground.ToString());
                if (color.Equals(textColor.Name))
                    cmbTextColor.SelectedItem = color;
            }

            txtFontSize.Text = editLabel.FontSize.ToString();
        }

        private void btnEditText_Click(object sender, RoutedEventArgs e)
        {
            double fontSize;

            Double.TryParse(txtFontSize.Text, out fontSize);

            if (fontSize <= 0)
                fontSize = 14;

            SolidColorBrush textColor = Brushes.White;


            if (cmbTextColor.SelectedItem != null)
                textColor = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbTextColor.SelectedItem.ToString());

            editLabel.Foreground = textColor;
            editLabel.FontSize = fontSize;
            this.Close();
        }
    }
}
