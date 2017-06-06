using Polybool.Net.Objects;

namespace Polybool.Net.Logic
{
    public static class PolyBool
    {
        public static PolySegments Segments(this Poligon poly)
        {
            var i = new Intersecter.RegionIntersecter();
            foreach (var region in poly.Regions)
            {
                i.AddRegion(region);
            }

            return new PolySegments()
            {
                Segments = i.Calculate(poly.Inverted),
                IsInverted = poly.Inverted
            };

        }
    }
}