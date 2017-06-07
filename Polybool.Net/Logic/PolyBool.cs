using Polybool.Net.Objects;

namespace Polybool.Net.Logic
{
    public static class PolyBool
    {
        public static PolySegments Segments(Poligon poly)
        {
            var i = new Intersecter.RegionIntersecter();
            foreach (var region in poly.Regions)
            {
                i.AddRegion(region);
            }

            return new PolySegments
            {
                Segments = i.Calculate(poly.Inverted),
                IsInverted = poly.Inverted
            };        
        }

        public static CombinedPolySegments Combine(PolySegments segments1, PolySegments segments2)
        {
            var i =new  Intersecter.SegmentIntersecter();
            return new CombinedPolySegments
            {
                Combined = i.Calculate(segments1.Segments, segments1.IsInverted, segments2.Segments, segments2.IsInverted),
                IsInverted1 = segments1.IsInverted,
                IsInverted2= segments2.IsInverted
    
            };
        }
    }
}