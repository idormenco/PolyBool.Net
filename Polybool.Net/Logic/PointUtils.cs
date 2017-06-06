using Polybool.Net.Objects;

namespace Polybool.Net.Logic
{
    public static class PointUtils
    {
        public static bool PointAboveOrOnLine(Point point, Point left, Point right)
        {
            var ax = left.X;
            var ay = left.Y;
            var bx = right.X;
            var by = right.Y;
            var cx = point.X;
            var cy = point.Y;
            return (bx - ax) * (cy - ay) - (by - ay) * (cx - ax) >= -Epsilon.Eps;
        }
    }
}