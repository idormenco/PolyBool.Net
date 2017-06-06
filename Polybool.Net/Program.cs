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
                            new Point(200,50),
                            new Point(600,50),
                            new Point(600,150),
                            new Point(200,150)
                        }
                    }

                }
            };
            var polySegments = poly.Segments();
            Console.Read();
        }
    }
}
