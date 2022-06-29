using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGridVisualization.Models
{
    public class PowerEntity
    {
        private long id;
        private string name;
        private double utmX;
        private double utmY;
        private double longitude;
        private double latitude;
        private int matrixRow;
        private int matrixCol;


        public PowerEntity() { }

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

        public double UtmX
        {
            get { return utmX; }
            set { utmX = value; }
        }

        public double UtmY
        {
            get { return utmY; }
            set { utmY = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public int MatrixRow
        {
            get { return matrixRow; }
            set { matrixRow = value; }
        }

        public int MatrixCol
        {
            get { return matrixCol; }
            set { matrixCol = value; }
        }

        public override string ToString()
        {
            return $"Entity ID = [{Id}]\n" +
                   $"Name = [{Name}]\n" +
                   $"MatrixRow = [{MatrixRow}]\n" +
                   $"MatrixCol = [{MatrixCol}]";
        }
    }
}
