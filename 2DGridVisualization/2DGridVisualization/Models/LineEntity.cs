using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _2DGridVisualization.Models
{
    public class LineEntity
    {
        private long id;
        private string name;
        private long firstEnd;
        private long secondEnd;
        private int startMatrixRow;
        private int startMatrixCol;
        private int endMatrixRow;
        private int endMatrixCol;
        private bool isDrawn;

        public LineEntity() { }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public long FirstEnd
        {
            get { return firstEnd; }
            set { firstEnd = value; }
        }

        public long SecondEnd
        {
            get { return secondEnd; }
            set { secondEnd = value; }
        }

        public int StartMatrixRow
        {
            get { return startMatrixRow; }
            set { startMatrixRow = value; }
        }

        public int StartMatrixCol
        {
            get { return startMatrixCol; }
            set { startMatrixCol = value; }
        }

        public int EndMatrixRow
        {
            get { return endMatrixRow; }
            set { endMatrixRow = value; }
        }

        public int EndMatrixCol
        {
            get { return endMatrixCol; }
            set { endMatrixCol = value; }
        }

        public bool IsDrawn
        {
            get { return isDrawn; }
            set { isDrawn = value; }
        }

        public override string ToString()
        {
            return $"Line ID = [{Id}]\n" +
                   $"Name = [{Name}]\n" +
                   $"First End = [{FirstEnd}]\n" +
                   $"Second End = [{SecondEnd}]";
        }
    }
}
