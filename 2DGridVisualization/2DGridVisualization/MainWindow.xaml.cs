using _2DGridVisualization.DrawingWindows;
using _2DGridVisualization.EditWindows;
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
    public partial class MainWindow : Window
    {

        public bool drawEllipse = false;
        public bool drawPolygon = false;
        public bool addText = false;

        // ============ DrawingObjects ============
        public static Ellipse ellipse;
        public static TextBlock ellipseText;
        // Polygon will be displayed as canvas consisting of Polygon and Label
        public static Polygon polygon;
        public static PointCollection polygonPoints = new PointCollection();
        public static TextBlock polygonText;
        // Label AddTextx
        public static Label text;

        public static SolidColorBrush entityColor;

        UIElement undoRedoElement = new UIElement();
        List<UIElement> clearedElements = new List<UIElement>();
        public static bool cleared = false;

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
            Console.WriteLine("[ INFO ] Lines drawn using BFS on canvas = " + drawnLines);
            drawnLines = 0;
            
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
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            txtTimes.Text += String.Format("{0, -14} | {1, -9} | {2}\n", $"{mainCanvas.Width} X {mainCanvas.Height} px", $"{Data.matrixCols} X {Data.matrixRows}", elapsedTime);


            btnDrawGrid.IsEnabled = false;
        }

        // DRAWING ENTITIES AND LINES
        #region DRAWING ENTITIES AND LINES
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
            }
        }


        #endregion

        // EDIT (undo, redo, clear)
        #region EDIT (undo, redo, clear)
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (cleared)
            {
                foreach (UIElement element in clearedElements)
                {
                    mainCanvas.Children.Add(element);
                }
                clearedElements.Clear();
                cleared = false;
                btnRedo.IsEnabled = false;
            }
            else
            {
                mainCanvas.Children.Remove(undoRedoElement);
                btnRedo.IsEnabled = true;
            }
            btnUndo.IsEnabled = false;
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            btnUndo.IsEnabled = true;
            btnRedo.IsEnabled = false;
            mainCanvas.Children.Add(undoRedoElement);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in mainCanvas.Children)
                clearedElements.Add(element);

            // reset data
            Scale.PixelsToMatrixSize((int)mainCanvas.Width);

            mainCanvas.Children.Clear();

            cleared = true;
            btnUndo.IsEnabled = true;
            btnRedo.IsEnabled = false;
            btnDrawGrid.IsEnabled = true;
        }


        #endregion

        #region DRAWING (ellipse, polygon, text)


        private void btnDrawEllipse_Click(object sender, RoutedEventArgs e)
        {
            drawEllipse = true;
            drawPolygon = false;
            addText = false;
        }

        private void btnDrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            drawEllipse = false;
            drawPolygon = true;
            addText = false;
        }

        private void btnAddText_Click(object sender, RoutedEventArgs e)
        {
            drawEllipse = false;
            drawPolygon = false;
            addText = true;
        }
        #endregion

        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point startingPoint = Mouse.GetPosition(mainCanvas);

            if (drawEllipse)
            {
                Canvas ellipseCanvas = new Canvas();
                ellipseCanvas.Width = mainCanvas.ActualWidth;
                ellipseCanvas.Height = mainCanvas.ActualHeight;

                DrawEllipse drawEllipseWindow = new DrawEllipse();
                drawEllipseWindow.ShowDialog();

                if (ellipse != null)
                {
                    Canvas.SetLeft(ellipse, startingPoint.X);
                    Canvas.SetTop(ellipse, startingPoint.Y);
                    ellipseCanvas.Children.Add(ellipse);
                }

                if (ellipseText != null)
                {
                    ellipseText.TextWrapping = TextWrapping.WrapWithOverflow;

                    Canvas.SetLeft(ellipseText, startingPoint.X + ellipse.Width / 3);
                    Canvas.SetTop(ellipseText, startingPoint.Y + ellipse.Height / 3);
                    ellipseCanvas.Children.Add(ellipseText);
                }
                undoRedoElement = ellipseCanvas;
                btnUndo.IsEnabled = true;
                cleared = false;

                mainCanvas.Children.Add(ellipseCanvas);

                ellipse = null;
                ellipseText = null;
            }
            else if (drawPolygon)
            {
                polygonPoints.Add(Mouse.GetPosition(mainCanvas));

                // Put dots on canvas where Polygon will be drawn
                Ellipse dot = new Ellipse();
                dot.Width = 4;
                dot.Height = 4;
                dot.Fill = Brushes.HotPink;

                Canvas.SetLeft(dot, startingPoint.X - dot.Width / 2);
                Canvas.SetTop(dot, startingPoint.Y - dot.Height / 2);

                mainCanvas.Children.Add(dot);

                return;
            }
            else if (addText)
            {
                AddText addTextWindow = new AddText();
                addTextWindow.ShowDialog();

                if (text != null)
                {
                    Canvas.SetLeft(text, startingPoint.X);
                    Canvas.SetTop(text, startingPoint.Y);

                    undoRedoElement = text;
                    btnUndo.IsEnabled = true;
                    cleared = false;

                    mainCanvas.Children.Add(text);

                    text = null;
                }
            }
            else
            {
                if (e.Source is Polyline)
                {
                    EditEntityColor editEntityColorWindow = new EditEntityColor();
                    editEntityColorWindow.ShowDialog();

                    foreach (var uiElement in mainCanvas.Children.OfType<Rectangle>())
                    {
                        if (((PowerEntity)uiElement.Tag).MatrixRow == ((LineEntity)((Polyline)e.Source).Tag).StartMatrixRow && ((PowerEntity)uiElement.Tag).MatrixCol == ((LineEntity)((Polyline)e.Source).Tag).StartMatrixCol)
                        {
                            uiElement.Fill = entityColor;
                        }
                        if (((PowerEntity)uiElement.Tag).MatrixRow == ((LineEntity)((Polyline)e.Source).Tag).EndMatrixRow && ((PowerEntity)uiElement.Tag).MatrixCol == ((LineEntity)((Polyline)e.Source).Tag).EndMatrixCol)
                        {
                            uiElement.Fill = entityColor;
                        }
                    }
                }
            }

            drawEllipse = false;
            drawPolygon = false;
            addText = false;
        }

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawPolygon)
            {
                Canvas polygonCanvas = new Canvas();
                polygonCanvas.Width = mainCanvas.ActualWidth;
                polygonCanvas.Height = mainCanvas.ActualHeight;

                DrawPolygon drawPolygonWindow = new DrawPolygon();
                drawPolygonWindow.ShowDialog();

                if (polygon != null && polygonPoints.Count() > 0)
                {
                    polygon.Points = new PointCollection(polygonPoints);

                    polygonCanvas.Children.Add(polygon);
                }

                if (polygonText != null)
                {
                    double avgX = 0;
                    double avgY = 0;

                    foreach (var dot in polygonPoints)
                    {
                        avgX += dot.X;
                        avgY += dot.Y;
                    }

                    avgX /= polygonPoints.Count();
                    avgY /= polygonPoints.Count();

                    polygonText.Width = 100;
                    polygonText.Height = 100;
                    polygonText.TextWrapping = TextWrapping.WrapWithOverflow;

                    Canvas.SetLeft(polygonText, avgX - polygonText.Width / 2);
                    Canvas.SetTop(polygonText, avgY);
                    polygonCanvas.Children.Add(polygonText);
                }
                undoRedoElement = polygonCanvas;
                btnUndo.IsEnabled = true;
                cleared = false;

                mainCanvas.Children.Add(polygonCanvas);

                // delete preview dots
                for (int i = 0; i < polygonPoints.Count(); i++)
                    mainCanvas.Children.RemoveAt(mainCanvas.Children.Count - 2);

                polygonPoints.Clear();

                polygon = null;
                polygonText = null;

                drawPolygon = false;
            }
            else
            {
                // ======================== EDIT DRAWN OBJECTS ========================
                if (e.Source is Ellipse)
                {
                    EditEllipse editEllipseWindow = new EditEllipse(LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) as Canvas);
                    editEllipseWindow.ShowDialog();
                }
                else if (e.Source is Polygon)
                {
                    EditPolygon editPolygonWindow = new EditPolygon(LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) as Canvas);
                    editPolygonWindow.ShowDialog();
                }
                else if (e.Source is TextBlock && LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) is Canvas)
                {
                    Canvas canvas = (Canvas)LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject);
                    if (canvas.Children[0] is Ellipse)
                    {
                        EditEllipse editEllipseWindow = new EditEllipse(LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) as Canvas);
                        editEllipseWindow.ShowDialog();
                    }
                    if (canvas.Children[0] is Polygon)
                    {
                        EditPolygon editPolygonWindow = new EditPolygon(LogicalTreeHelper.GetParent(e.OriginalSource as DependencyObject) as Canvas);
                        editPolygonWindow.ShowDialog();
                    }
                }
                else if (e.Source is Label)
                {
                    EditText editTextWindow = new EditText((Label)e.Source);
                    editTextWindow.ShowDialog();
                }
            }
        }

        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            if((int)canvasGrid.ActualWidth > 0 && (int)canvasGrid.ActualHeight > 0)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvasGrid.ActualWidth + 300, (int)canvasGrid.ActualHeight, 96.0, 96.0, PixelFormats.Default);
                rtb.Render(canvasGrid);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));


                string filename = DateTime.Now.ToString("dd-M-yyyy_HH.mm.ss.ff") + ".jpg";

                System.IO.FileStream stream = System.IO.File.Create(filename);
                encoder.Save(stream);
                stream.Close();
                MessageBox.Show($"Screenshot \'{filename}\' saved!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Set canvas size first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
