﻿using _2DGridVisualization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGridVisualization
{
    public class BidirectionalBFS
    {
        public static List<MatrixCell> BidirectionalSearch(LineEntity line, char[,] linesMatrix)
        {
            linesMatrix[line.StartMatrixRow, line.StartMatrixCol] = 'S'; // 
            linesMatrix[line.EndMatrixRow, line.EndMatrixCol] = 'D'; // 

            MatrixCell source = new MatrixCell(line.StartMatrixRow, line.StartMatrixCol);
            MatrixCell destination = new MatrixCell(line.EndMatrixRow, line.EndMatrixCol);

            Queue<MatrixCell> sourceQueue = new Queue<MatrixCell>();
            Queue<MatrixCell> destinationQueue = new Queue<MatrixCell>();

            sourceQueue.Enqueue(source);
            destinationQueue.Enqueue(destination);

            bool[,] sourceVisited = new bool[Data.matrixRows, Data.matrixCols];
            bool[,] destinationVisited = new bool[Data.matrixRows, Data.matrixCols];

            MatrixCell[,] sourcePrevious = new MatrixCell[Data.matrixRows, Data.matrixCols];
            MatrixCell[,] destinationPrevious = new MatrixCell[Data.matrixRows, Data.matrixCols];

            for(int row = 0; row < Data.matrixRows; row++)
            {
                for (int col = 0; col < Data.matrixCols; col++)
                {
                    sourceVisited[row, col] = false;
                    destinationVisited[row, col] = false;
                    sourcePrevious[row, col] = null;
                    destinationPrevious[row, col] = null;
                }
            }

            sourceVisited[source.Row, source.Col] = true;
            destinationVisited[destination.Row, destination.Col] = true;


            // Bidirectional Search
            while (sourceQueue.Count != 0 && destinationQueue.Count != 0)
            {
                if (sourceQueue.Count != 0)
                {
                    MatrixCell currentCell = sourceQueue.Dequeue();

                    if (currentCell == destination || destinationQueue.Contains(currentCell))
                    {
                        // Path Found
                        linesMatrix[(int)line.StartMatrixRow, (int)line.StartMatrixCol] = ' ';
                        linesMatrix[(int)line.EndMatrixRow, (int)line.EndMatrixCol] = ' ';
                        List<MatrixCell> jedan = ReconstructPath(source, currentCell, sourcePrevious);
                        List<MatrixCell> dva = ReconstructPath(destination, currentCell, destinationPrevious);
                        dva.Reverse();
                        dva.RemoveAt(0);
                        List<MatrixCell> ret = (List<MatrixCell>)jedan.Concat(dva).ToList();
                        return ret;
                    }
                    else
                    {
                        foreach(MatrixCell cell in GetNeighborCells(currentCell))
                        {
                            if(isValid(cell.Row, cell.Col, sourceVisited, linesMatrix))
                            {
                                sourceQueue.Enqueue(new MatrixCell(cell.Row, cell.Col));
                                sourceVisited[cell.Row, cell.Col] = true;
                                sourcePrevious[cell.Row, cell.Col] = currentCell;
                            }
                        }
                    }
                }

                if (destinationQueue.Count != 0)
                {
                    MatrixCell currentCell = destinationQueue.Dequeue();

                    if (currentCell == source || sourceQueue.Contains(currentCell))
                    {
                        // Path Found
                        linesMatrix[line.StartMatrixRow, line.StartMatrixCol] = ' ';
                        linesMatrix[line.EndMatrixRow, line.EndMatrixCol] = ' ';
                        List<MatrixCell> jedan = ReconstructPath(source, currentCell, sourcePrevious);
                        List<MatrixCell> dva = ReconstructPath(destination, currentCell, destinationPrevious);
                        dva.Reverse();
                        dva.RemoveAt(0);
                        List<MatrixCell> ret = (List<MatrixCell>)jedan.Concat(dva).ToList();
                        return ret;
                    }
                    else
                    {
                        foreach (MatrixCell cell in GetNeighborCells(currentCell))
                        {
                            if (isValid(cell.Row, cell.Col, destinationVisited, linesMatrix))
                            {
                                destinationQueue.Enqueue(new MatrixCell(cell.Row, cell.Col));
                                destinationVisited[cell.Row, cell.Col] = true;
                                destinationPrevious[cell.Row, cell.Col] = currentCell;
                            }
                        }
                    }
                }
            }

            // Path Not Found
            return null;
        }

        private static bool isValid(int row, int col, bool[,] visited, char[,] linesMatrix)
        {
            if (row >= 0 && col >= 0 && row < Data.matrixRows && col < Data.matrixCols && linesMatrix[row, col] != 'X' && visited[row, col] == false)
                return true;
            else
                return false;
        }

        private static List<MatrixCell> GetNeighborCells(MatrixCell cell)
        {
            List<MatrixCell> neighborCells = new List<MatrixCell>();
            neighborCells.Add(new MatrixCell(cell.Row - 1, cell.Col));
            neighborCells.Add(new MatrixCell(cell.Row + 1, cell.Col));
            neighborCells.Add(new MatrixCell(cell.Row, cell.Col - 1));
            neighborCells.Add(new MatrixCell(cell.Row, cell.Col + 1));
            return neighborCells;
        }

        public static List<MatrixCell> ReconstructPath(MatrixCell source, MatrixCell destination, MatrixCell[,] prev)
        {
            List<MatrixCell> path = new List<MatrixCell>();

            for (MatrixCell cell = destination; cell != null; cell = prev[cell.Row, cell.Col])
            {
                path.Add(cell);
            }

            path.Reverse();

            if (path[0].Row == source.Row && path[0].Col == source.Col)
                return path;
            else
                return null;
        }
    }
}
