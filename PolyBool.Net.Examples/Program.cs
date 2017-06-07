using System;
using System.Collections.Generic;
using Polybool.Net.Logic;
using Polybool.Net.Objects;

namespace PolyBool.Net.Examples
{
    internal class Program
    {
        private static void Main()
        {
            var poly1 = new Polygon
            {
                Regions = new List<Region>
                { new Region{
                    Points = new List<Point>
                        {
                            new Point(200L,50L),
                            new Point(600L,50L),
                            new Point(600L,150L),
                            new Point(200L,150L)
                        }
                    }

                }
            };
            var poly2 = new Polygon
            {
                Regions = new List<Region>
                { new Region{
                    Points = new List<Point>
                        {
                            new Point(300L,150L),
                            new Point(500L,90L),
                            new Point(500L,200L),
                            new Point(300L,200L)
                        }
                    }

                }
            };
            var seg1 = Polybool.Net.Logic.PolyBool.Segments(poly1);
            var seg2 = Polybool.Net.Logic.PolyBool.Segments(poly2);
            var comb = Polybool.Net.Logic.PolyBool.Combine(seg1, seg2);
            var seg3 = SegmentSelector.Difference(comb);
            var pol = Polybool.Net.Logic.PolyBool.Polygon(seg3);
            Console.WriteLine(pol);
        }
    }
}
