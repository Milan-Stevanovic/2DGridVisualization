using _2DGridVisualization.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static int drawnLines = 0;
        public MainWindow()
        {
            InitializeComponent();
            btnDrawGrid.IsEnabled = false;
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
            btnDrawGrid.IsEnabled = true;
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
            if (Data.matrix == null || Data.allEntities.Count() == 0)
            {
                MessageBox.Show("You must set size and load entities before drawing!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Clear canvas and recalculate positions
            mainCanvas.Children.Clear();

            Scale.ScaleToCanvas();
            foreach (PowerEntity powerEntity in Data.allEntities.Values)
            {
                bool found = false;
                int step = 0;
                while (!found)
                {
                    found = Scale.CheckField(powerEntity, (int)powerEntity.MatrixRow, (int)powerEntity.MatrixCol, step);
                    step++;
                }
            }
            Scale.SetLinesStartAndEnd();

            // Drawing Entities
            for (int row = 0; row < Data.matrixRows; row++)
            {
                for (int col = 0; col < Data.matrixCols; col++)
                {
                    if (Data.matrix[row, col] != null)
                    {
                        DrawEntity(row, col);
                    }
                }
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();


            // Drawing Lines
            foreach (LineEntity line in Data.lines.Values)
            {
                if(checkBoxDrawNodes.IsChecked == false)
                {
                    if(Data.matrix[line.StartMatrixRow, line.StartMatrixCol] is NodeEntity) // check if FirstEnd is type NodeEntity
                    {
                        continue;
                    }
                }
                
                BFSDrawLine(line);
            }
            Console.WriteLine("Lines drawn using BFS on canvas = " + drawnLines);
            
            foreach (LineEntity line in Data.lines.Values)
            {
                if (!line.IsDrawn)
                {
                    if (checkBoxDrawNodes.IsChecked == false)
                    {
                        if (Data.matrix[line.StartMatrixRow, line.StartMatrixCol] is NodeEntity) // check if FirstEnd is type NodeEntity
                        {
                            continue;
                        }
                    }
                    
                    DrawLine(line);
                }
            }
            


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",  ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            txtTimes.Text += String.Format("{0, -15} | {1, -10} | {2}\n", $"{mainCanvas.Width} X {mainCanvas.Height} px", $"{Data.matrixCols} X {Data.matrixRows}", elapsedTime);


            btnDrawGrid.IsEnabled = false;
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

        private void BFSDrawLine(LineEntity line)
        {
            List<MatrixCell> path = BidirectionalBFS.BidirectionalSearch(line, Data.linesMatrix);

            if (path == null)
            {
                // Cannot reconstruct path
                line.IsDrawn = false;
            }
            else
            {
                // FOUND PATH
                line.IsDrawn = true;

                Polyline polyLine = new Polyline();
                polyLine.Stroke = Brushes.Orange;
                polyLine.StrokeThickness = Data.entitySize / 4F;
                polyLine.ToolTip = line.ToString();
                polyLine.Tag = line;
                PointCollection pointCollection = new PointCollection();

                foreach (MatrixCell cell in path)
                {
                    Data.linesMatrix[cell.Row, cell.Col] = 'X';
                    pointCollection.Add(new System.Windows.Point(cell.Col * Data.gridCellSize + Data.entitySize / 2, cell.Row * Data.gridCellSize + Data.entitySize / 2));
                }

                polyLine.Points = pointCollection;
                mainCanvas.Children.Add(polyLine);
                drawnLines++;
            }
        }

        private void DrawLine(LineEntity line)
        {
            List<MatrixCell> path = BidirectionalBFS.BidirectionalSearch(line, new char[Data.matrixRows, Data.matrixCols]);

            if (path == null)
            {
                // Cannot reconstruct path
                line.IsDrawn = false;
            }
            else
            {
                // FOUND PATH
                line.IsDrawn = true;

                Polyline polyLine = new Polyline();
                polyLine.Stroke = Brushes.GreenYellow;
                polyLine.StrokeThickness = Data.entitySize / 2F;
                polyLine.ToolTip = line.ToString();
                polyLine.Tag = line;
                PointCollection pointCollection = new PointCollection();

                foreach (MatrixCell cell in path)
                {
                    if (Data.linesMatrix[cell.Row, cell.Col] == 'X')
                    {
                        Ellipse overlapDot = new Ellipse();
                        overlapDot.Height = Data.entitySize / 2;
                        overlapDot.Width = Data.entitySize / 2;
                        overlapDot.Fill = Brushes.White;
                        Canvas.SetLeft(overlapDot, cell.Col * Data.gridCellSize + Data.entitySize / 4);
                        Canvas.SetTop(overlapDot, cell.Row * Data.gridCellSize + Data.entitySize / 4);
                        mainCanvas.Children.Add(overlapDot);
                    }
                    pointCollection.Add(new System.Windows.Point(cell.Col * Data.gridCellSize + Data.entitySize / 2, cell.Row * Data.gridCellSize + Data.entitySize / 2));
                }

                polyLine.Points = pointCollection;
                mainCanvas.Children.Add(polyLine);
                drawnLines++;
            }
        }
    }
}
