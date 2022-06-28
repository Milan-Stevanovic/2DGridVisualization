using _2DGridVisualization.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2DGridVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lblCanvasSize.Content = $"{mainCanvas.Width} X {mainCanvas.Height} px";
        }

        private void btnSetCanvasSize_Click(object sender, RoutedEventArgs e)
        {
            int width;

            Int32.TryParse(txtCanvasWidth.Text, out width);

            if(width < 300 || width > 2250)
            {
                MessageBox.Show("Width must be between 300 and 2250 pixels!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mainCanvas.Width = width;
            mainCanvas.Height = Math.Floor(width / 1.5);

            Scale.PixelsToMatrixSize(width);

            lblMatrixSize.Content = $"{Data.matrixCols} X {Data.matrixRows}";
            lblCanvasSize.Content = $"{mainCanvas.Width} X {mainCanvas.Height} px";

            mainCanvas.Children.Clear();
        }

        private void txtCanvasWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            int width;
            Int32.TryParse(txtCanvasWidth.Text, out width);

            txtCanvasHeight.Text = (Math.Floor(width / 1.5F)).ToString();
        }

        private void btnLoadGrid_Click(object sender, RoutedEventArgs e)
        {
            Loader.LoadAllEntities();
            btnLoadGrid.IsEnabled = false;
            MessageBox.Show("All Entities Loaded", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnDrawGrid_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();

            Scale.ScaleToCanvas();

            foreach (PowerEntity powerEntity in Data.allEntities.Values)
            {
                bool found = false;
                int step = 0;
                while (!found)
                {
                    found = Scale.CheckField(powerEntity, (int)Math.Floor(powerEntity.MatrixRow), (int)Math.Floor(powerEntity.MatrixCol), step);
                    step++;
                }
            }


            int drawnEntities = 0;

            for (int row = 0; row < Data.matrixRows; row++)
            {
                for (int col = 0; col < Data.matrixCols; col++)
                {
                    if (Data.matrix[row, col] != null)
                    {
                        DrawEntity(row, col);
                        drawnEntities++;
                    }
                }
            }
            Console.WriteLine("Entities drawn on canvas = " + drawnEntities);
        }

        private void DrawEntity(int row, int col)
        {
            Rectangle rect = new Rectangle();
            rect.Fill = Brushes.Red;
            rect.Width = Data.entitySize;
            rect.Height = Data.entitySize;
            rect.Stroke = Brushes.Black;
            rect.StrokeThickness = 0.2;
            rect.Tag = Data.matrix[row, col];

            if (Data.matrix[row, col] is SubstationEntity)
            {
                rect.ToolTip = "= Substation =\n";
                rect.Fill = Brushes.White;
            }
            if (Data.matrix[row, col] is NodeEntity)
            {
                rect.ToolTip = "= Node =\n";
                rect.Fill = Brushes.DeepSkyBlue;
            }
            if (Data.matrix[row, col] is SwitchEntity)
            {
                rect.ToolTip = "= Switch =\n";
                rect.Fill = Brushes.MediumPurple;
            }
            rect.ToolTip += Data.matrix[row, col].ToString();


            Canvas.SetTop(rect, row * Data.gridCellSize);
            Canvas.SetLeft(rect, col * Data.gridCellSize);

            mainCanvas.Children.Add(rect);
        }
    }
}
