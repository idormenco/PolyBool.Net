using System.Collections.Generic;

namespace Polybool.Net.Objects
{
    public class Polygon
    {
        public Polygon()
        {
            Regions = new List<Region>();
        }

        public List<Region> Regions { get; set; }
        public bool Inverted { get; set; }
    }
}