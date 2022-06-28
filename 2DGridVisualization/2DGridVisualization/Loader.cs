using _2DGridVisualization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2DGridVisualization
{
    public class Loader
    {
        public static void LoadAllEntities()
        {
            double newX, newY;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Geographic.xml");

            XmlNodeList nodeList;

            // Loading Substations
            SubstationEntity sub = new SubstationEntity();

            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");
            foreach (XmlNode node in nodeList)
            {
                sub.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                sub.Name = node.SelectSingleNode("Name").InnerText;
                sub.UtmX = double.Parse(node.SelectSingleNode("X").InnerText);
                sub.UtmY = double.Parse(node.SelectSingleNode("Y").InnerText);

                ToLatLon(sub.UtmX, sub.UtmY, 34, out newX, out newY);

                Data.allEntities.Add(sub.Id,
                    new SubstationEntity
                    {
                        Id = sub.Id,
                        Name = sub.Name,
                        UtmX = sub.UtmX,
                        UtmY = sub.UtmY,
                        Latitude = newX,
                        Longitude = newY
                    }
                );
            }


            // Loading Nodes
            NodeEntity nodeobj = new NodeEntity();

            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");
            foreach (XmlNode node in nodeList)
            {
                nodeobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                nodeobj.Name = node.SelectSingleNode("Name").InnerText;
                nodeobj.UtmX = double.Parse(node.SelectSingleNode("X").InnerText);
                nodeobj.UtmY = double.Parse(node.SelectSingleNode("Y").InnerText);

                ToLatLon(nodeobj.UtmX, nodeobj.UtmY, 34, out newX, out newY);

                Data.allEntities.Add(nodeobj.Id, 
                    new NodeEntity
                    {
                        Id = nodeobj.Id,
                        Name = nodeobj.Name,
                        UtmX = nodeobj.UtmX,
                        UtmY = nodeobj.UtmY,
                        Latitude = newX,
                        Longitude = newY
                    }
                );
            }


            // Loading Swithces
            SwitchEntity switchobj = new SwitchEntity();

            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");
            foreach (XmlNode node in nodeList)
            {
                switchobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                switchobj.Name = node.SelectSingleNode("Name").InnerText;
                switchobj.UtmX = double.Parse(node.SelectSingleNode("X").InnerText);
                switchobj.UtmY = double.Parse(node.SelectSingleNode("Y").InnerText);

                ToLatLon(switchobj.UtmX, switchobj.UtmY, 34, out newX, out newY);

                Data.allEntities.Add(switchobj.Id,
                    new SwitchEntity
                    {
                        Id = switchobj.Id,
                        Name = switchobj.Name,
                        UtmX = switchobj.UtmX,
                        UtmY = switchobj.UtmY,
                        Latitude = newX,
                        Longitude = newY
                    }
                );
            }

            Data.minLongitude = Data.allEntities.Min(x => x.Value.Longitude);
            Data.maxLongitude = Data.allEntities.Max(x => x.Value.Longitude);
            Data.minLatitude = Data.allEntities.Min(x => x.Value.Latitude);
            Data.maxLatitude = Data.allEntities.Max(x => x.Value.Latitude);

        


            // Loading Lines
            // ...
        }

        //From UTM to Latitude and longitude in decimal
        public static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = true;

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = zoneUTM;
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }
    }
}
