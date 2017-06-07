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
            var poly1 = new Poligon
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
            var poly2 = new Poligon
            {
                Regions = new List<Region>
                { new Region{
                    Points = new List<Point>
                        {
                            new Point(300,150),
                            new Point(500,100),
                            new Point(500,200),
                            new Point(300,200)
                        }
                    }

                }
            };
            var seg1 = PolyBool.Segments(poly1);
            var seg2 = PolyBool.Segments(poly2);
            var comb = PolyBool.Combine(seg1, seg2);
            List<Segment> difference = SegmentSelector.Difference(comb.Combined);
            Console.Read();
        }
    }
}
