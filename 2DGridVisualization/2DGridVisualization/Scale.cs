using _2DGridVisualization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGridVisualization
{
    public class Scale
    {
        public static void PixelsToMatrixSize(int widthPx)
        {
            // Scale canvas resolution to matrix
            Data.matrixCols = (Data.maxMatrixCol - Data.minMatrixCol) * (widthPx - Data.minCanvasPxWidth) / (Data.maxCanvasPxWidth - Data.minCanvasPxWidth) + Data.minMatrixCol;
            Data.matrixRows = Convert.ToInt32(Math.Floor(Data.matrixCols / 1.5F));

            // Set data
            Data.matrix = new PowerEntity[Data.matrixRows, Data.matrixCols];
            Data.gridCellSize = (double)widthPx / Data.matrixCols;
            Data.entitySize = Data.gridCellSize / 2F;
        }

        public static void ScaleToCanvas()
        {
            for (int i = 0; i < Data.allEntities.Count; i++)
            {
                Data.allEntities.ElementAt(i).Value.MatrixRow = (int)(Data.matrixRows - (Data.matrixRows * (Data.allEntities.ElementAt(i).Value.Latitude - Data.minLatitude) / (Data.maxLatitude - Data.minLatitude)));
                Data.allEntities.ElementAt(i).Value.MatrixCol = (int)(Data.matrixCols * (Data.allEntities.ElementAt(i).Value.Longitude - Data.minLongitude) / (Data.maxLongitude - Data.minLongitude));
            }
        }

        public static bool CheckField(PowerEntity entity, int row, int col, int step)
        {
            for (int i = -step; i <= step; i++) // rows
            {
                for (int j = -step; j <= step; j++) // cols
                {
                    if (col + j < Data.matrixCols && col + j > -1 && row + i < Data.matrixRows && row + i > -1)
                    {
                        if (Data.matrix[row + i, col + j] == null)
                        {
                            // Found empty field
                            Data.matrix[row + i, col + j] = entity;
                            Data.allEntities[entity.Id].MatrixRow = row + i;
                            Data.allEntities[entity.Id].MatrixCol = col + j;
                            return true;
                        }
                    }
                }
            }
            return false; // Not found
        }

        public static void SetLinesStartAndEnd()
        {
            foreach (LineEntity line in Data.lines.Values)
            {
                line.StartMatrixCol = Data.allEntities[line.FirstEnd].MatrixCol;
                line.StartMatrixRow = Data.allEntities[line.FirstEnd].MatrixRow;
                line.EndMatrixCol = Data.allEntities[line.SecondEnd].MatrixCol;
                line.EndMatrixRow = Data.allEntities[line.SecondEnd].MatrixRow;
            }
        }
    }
}
