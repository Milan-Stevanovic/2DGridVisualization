using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGridVisualization.Models
{
    public class MatrixCell
    {
        int row;
        int col;

        public MatrixCell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public override string ToString()
        {
            return $"({Row}, {Col})";
        }

        public override bool Equals(object obj)
        {
            return (this.Row == ((MatrixCell)obj).Row &&
                    this.Col == ((MatrixCell)obj).Col);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
