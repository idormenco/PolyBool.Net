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
            var poly1 = new Polygon
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
            var poly2 = new Polygon
            {
                Regions = new List<Region>
                { new Region{
                    Points = new List<Point>
                        {
                            new Point(300L,150L),
                            new Point(500L,100L),
                            new Point(500L,200L),
                            new Point(300L,200L)
                        }
                    }

                }
            };
            var seg1 = PolyBool.Segments(poly1);
            var seg2 = PolyBool.Segments(poly2);
            var comb = PolyBool.Combine(seg1, seg2);
            var seg3 = SegmentSelector.Difference(comb);
            var pol = PolyBool.Polygon(seg3);
            int a = 0;
        }
    }
}
