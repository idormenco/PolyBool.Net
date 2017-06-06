using System.Collections.Generic;

namespace Polybool.Net.Objects
{
    public class Poligon
    {
        public Poligon()
        {
            Regions = new List<Region>();
        }

        public Poligon(List<Region> regions, bool inverted)
        {
            Regions = regions;
            Inverted = inverted;
        }

        public List<Region> Regions { get; set; }
        public bool Inverted { get; set; }
    }
}