using Polybool.Net.Objects;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Polybool.Net.Logic
{
    [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
    public static class SegmentSelector
    {
        public static List<Segment> Union(List<Segment> segments)
        {
            return Select(segments, new[] {   0, 2, 1, 0,
                2, 2, 0, 0,
                1, 0, 1, 0,
                0, 0, 0, 0
            });
        }

        public static List<Segment> Intersect(List<Segment> segments)
        {
            return Select(segments, new[] {   0, 0, 0, 0,
                0, 2, 0, 2,
                0, 0, 1, 1,
                0, 2, 1, 0
            });
        }

        public static PolySegments Difference(CombinedPolySegments combined)
        {
            return new PolySegments
            {
                Segments = Select(combined.Combined, new[]
                {
                    0, 0, 0, 0,
                    2, 0, 2, 0,
                    1, 1, 0, 0,
                    0, 1, 2, 0
                }),
                IsInverted = !combined.IsInverted1 && combined.IsInverted2
            };
        }

        public static List<Segment> DifferenceRev(List<Segment> segments)
        {
            return Select(segments, new[] {   0, 2, 1, 0,
                0, 0, 1, 1,
                0, 2, 0, 2,
                0, 0, 0, 0
            });
        }

        public static List<Segment> Xor(List<Segment> segments)
        {
            return Select(segments, new[] {   0, 2, 1, 0,
                2, 0, 0, 1,
                1, 0, 0, 2,
                0, 1, 2, 0
            });
        }

        private static List<Segment> Select(List<Segment> segments, int[] selection)
        {
            List<Segment> result = new List<Segment>();

            foreach (Segment segment in segments)
            {
                int index = (segment.MyFill.Above.Value ? 8 : 0) +
                            (segment.MyFill.Below.Value ? 4 : 0) +
                            (segment.OtherFill != null && segment.OtherFill.Above.Value ? 2 : 0) +
                            (segment.OtherFill != null && segment.OtherFill.Below.Value ? 1 : 0);

                if (selection[index] != 0)
                {
                    result.Add(new Segment
                    {
                        Start = segment.Start,
                        End = segment.End,
                        MyFill = new Fill
                        {
                            Above = selection[index] == 1,
                            Below = selection[index] == 2
                        }
                    });
                }
            }

            return result;
        }
    }
}