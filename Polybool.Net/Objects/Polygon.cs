using System.Collections.Generic;

namespace Polybool.Net.Objects
{
    public class Polygon
    {
        public Polygon()
        {
            Regions = new List<Region>();
        }

        public Polygon(List<Region> regions, bool isInverted = false)
        {
            Regions = regions;
            Inverted = isInverted;
        }

        public List<Region> Regions { get; set; }
        public bool Inverted { get; set; }
    }
}