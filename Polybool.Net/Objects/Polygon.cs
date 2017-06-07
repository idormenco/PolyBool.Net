using System.Collections.Generic;

namespace Polybool.Net.Objects
{
    public class Polygon
    {
        public Polygon()
        {
            Regions = new List<Region>();
        }

        public Polygon(List<Region> regions, bool inverted)
        {
            Regions = regions;
            Inverted = inverted;
        }

        public List<Region> Regions { get; set; }
        public bool Inverted { get; set; }
    }
}