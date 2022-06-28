using _2DGridVisualization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGridVisualization
{
    public class Data
    {
        public static int minCanvasPxWidth = 300;
        public static int maxCanvasPxWidth = 2250;

        public static int minMatrixCol = 100;
        public static int maxMatrixCol = 500;
        public static int minMatrixRow = Convert.ToInt32(Math.Floor(minMatrixCol / 1.5F));
        public static int maxMatrixRow = Convert.ToInt32(Math.Floor(maxMatrixCol / 1.5F));


        public static int matrixRows;
        public static int matrixCols;

        public static double gridCellSize;
        public static double entitySize;

        public static PowerEntity[,] matrix;

        public static Dictionary<long, PowerEntity> allEntities = new Dictionary<long, PowerEntity>();


        public static double minLongitude;
        public static double maxLongitude;
        public static double minLatitude;
        public static double maxLatitude;
    }
}
