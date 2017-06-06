using System;
using System.Collections.Generic;
using System.ComponentModel;
using Polybool.Net.Logic;
using Polybool.Net.Objects;

namespace Polybool.Net
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var poly = new Poligon
            {
                Regions = new List<Region>
                { new Region{
                    Points = new List<Point>
                        {
                            new Point(500,60),
                            new Point(500,150),
                            new Point(460,190),
                            new Point(460,110),
                            new Point(400,180),
                            new Point(160,90)
                        }
                    }
                }
            };
            var polySegments = poly.Segments();
            Console.Read();
        }
    }
}
