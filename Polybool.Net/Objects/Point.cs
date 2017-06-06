namespace Polybool.Net.Objects
{
    public class Point
    {
        public Point(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}