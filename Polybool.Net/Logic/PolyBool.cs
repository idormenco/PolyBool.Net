using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Polybool.Net.Objects;

namespace Polybool.Net.Logic
{
    public static class PolyBool
    {

        private static List<Region> SegmentChainer(List<Segment> segments)
        {
            var regions = new List<Region>();
            var chains = new List<Point[]>();

            foreach (Segment seg in segments)
            {
                var pt1 = seg.Start;
                var pt2 = seg.End;
                if (PointUtils.PointsSame(pt1, pt2))
                {
                    Debug.WriteLine("PolyBool: Warning: Zero-length segment detected; your epsilon is " +
                        "probably too small or too large");
                    continue;
                }

                // search for two chains that this segment matches
                var first_match = new Matcher()
                {
                    index = 0,
                    matches_head = false,
                    matches_pt1 = false

                };
                var second_match = new Matcher()
                {
                    index = 0,
                    matches_head = false,
                    matches_pt1 = false

                };
                var next_match = first_match;

                Func<int, bool, bool, bool> setMatch = (index, matches_head, matches_pt1) =>
                 {
                    // return true if we've matched twice
                    next_match.index = index;
                     next_match.matches_head = matches_head;
                     next_match.matches_pt1 = matches_pt1;
                     if (Equals(next_match, first_match))
                     {
                         next_match = second_match;
                         return false;
                     }
                     next_match = null;
                     return true; // we've matched twice, we're done here
                };


                for (var i = 0; i < chains.Count; i++)
                {
                    var chain = chains[i];
                    var head = chain[0];
                    var head2 = chain[1];
                    var tail = chain[chain.Length - 1];
                    var tail2 = chain[chain.Length - 2];
                    if (PointUtils.PointsSame(head, pt1))
                    {
                        if (setMatch(i, true, true))
                            break;
                    }
                    else if (PointUtils.PointsSame(head, pt2))
                    {
                        if (setMatch(i, true, false))
                            break;
                    }
                    else if (PointUtils.PointsSame(tail, pt1))
                    {
                        if (setMatch(i, false, true))
                            break;
                    }
                    else if (PointUtils.PointsSame(tail, pt2))
                    {
                        if (setMatch(i, false, false))
                            break;
                    }
                }

                if (Equals(next_match, first_match))
                {
                    // we didn't match anything, so create a new chain
                    chains.Add(new[] { pt1, pt2 });
                    continue;
                }

                if (Equals(next_match, second_match))
                {
                    // we matched a single chain

                    // add the other point to the apporpriate end, and check to see if we've closed the
                    // chain into a loop

                    var index = first_match.index;
                    var pt = first_match.matches_pt1 ? pt2 : pt1; // if we matched pt1, then we add pt2, etc
                    var addToHead = first_match.matches_head; // if we matched at head, then add to the head

                    var chain = chains[index];
                    var grow = addToHead ? chain[0] : chain[chain.Length - 1];
                    var grow2 = addToHead ? chain[1] : chain[chain.Length - 2];
                    var oppo = addToHead ? chain[chain.Length - 1] : chain[0];
                    var oppo2 = addToHead ? chain[chain.Length - 2] : chain[1];

                    if (PointUtils.PointsCollinear(grow2, grow, pt))
                    {
                        // grow isn't needed because it's directly between grow2 and pt:
                        // grow2 ---grow---> pt
                        if (addToHead)
                        {
                            chain.Shift();
                        }
                        else
                        {
                            chain.Pop();
                        }
                        grow = grow2; // old grow is gone... new grow is what grow2 was
                    }

                    if (PointUtils.PointsSame(oppo, pt))
                    {
                        // we're closing the loop, so remove chain from chains
                        chains.Splice(index, 1);

                        if (PointUtils.PointsCollinear(oppo2, oppo, grow))
                        {
                            // oppo isn't needed because it's directly between oppo2 and grow:
                            // oppo2 ---oppo--->grow
                            if (addToHead)
                            {
                                chain.Pop();
                            }
                            else
                            {
                                chain.Shift();
                            }
                        }

                        // we have a closed chain!
                        regions.Add(new Region() {Points = chain.ToList()});
                        continue;
                    }

                    // not closing a loop, so just add it to the apporpriate side
                    if (addToHead)
                    {
                        chain = chain.Unshift(pt);
                    }
                    else
                    {
                        chain = chain.Push(pt);
                    }
                    continue;
                }

                // otherwise, we matched two chains, so we need to combine those chains together

                Action<int> reverseChain = (index) =>
                {
                    chains[index].Reverse(); // gee, that's easy
                };

                Action<int, int> appendChain = (index1, index2) =>
                {
                    // index1 gets index2 appended to it, and index2 is removed
                    var chain1 = chains[index1];
                    var chain2 = chains[index2];
                    var tail = chain1[chain1.Length - 1];
                    var tail2 = chain1[chain1.Length - 2];
                    var head = chain2[0];
                    var head2 = chain2[1];

                    if (PointUtils.PointsCollinear(tail2, tail, head))
                    {
                        // tail isn't needed because it's directly between tail2 and head
                        // tail2 ---tail---> head
                        chain1.Pop();
                        tail = tail2; // old tail is gone... new tail is what tail2 was
                    }

                    if (PointUtils.PointsCollinear(tail, head, head2))
                    {
                        // head isn't needed because it's directly between tail and head2
                        // tail ---head---> head2
                        chain2.Shift();
                    }
                    chains[index1] = chain1.Concat(chain2).ToArray();
                    chains.Splice(index2, 1);
                };

                var F = first_match.index;
                var S = second_match.index;


                var reverseF = chains[F].Length < chains[S].Length; // reverse the shorter chain, if needed
                if (first_match.matches_head)
                {
                    if (second_match.matches_head)
                    {
                        if (reverseF)
                        {
                            // <<<< F <<<< --- >>>> S >>>>
                            reverseChain(F);
                            // >>>> F >>>> --- >>>> S >>>>
                            appendChain(F, S);
                        }
                        else
                        {
                            // <<<< F <<<< --- >>>> S >>>>
                            reverseChain(S);
                            // <<<< F <<<< --- <<<< S <<<<   logically same as:
                            // >>>> S >>>> --- >>>> F >>>>
                            appendChain(S, F);
                        }
                    }
                    else
                    {
                        // <<<< F <<<< --- <<<< S <<<<   logically same as:
                        // >>>> S >>>> --- >>>> F >>>>
                        appendChain(S, F);
                    }
                }
                else
                {
                    if (second_match.matches_head)
                    {
                        // >>>> F >>>> --- >>>> S >>>>
                        appendChain(F, S);
                    }
                    else
                    {
                        if (reverseF)
                        {
                            // >>>> F >>>> --- <<<< S <<<<
                            reverseChain(F);
                            // <<<< F <<<< --- <<<< S <<<<   logically same as:
                            // >>>> S >>>> --- >>>> F >>>>
                            appendChain(S, F);
                        }
                        else
                        {
                            // >>>> F >>>> --- <<<< S <<<<
                            reverseChain(S);
                            // >>>> F >>>> --- >>>> S >>>>
                            appendChain(F, S);
                        }
                    }
                }
            }

            return regions;
        }

        public static PolySegments Segments(Polygon poly)
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
            var i = new Intersecter.SegmentIntersecter();
            return new CombinedPolySegments
            {
                Combined = i.Calculate(segments1.Segments, segments1.IsInverted, segments2.Segments, segments2.IsInverted),
                IsInverted1 = segments1.IsInverted,
                IsInverted2 = segments2.IsInverted

            };
        }

        public static Polygon Polygon(PolySegments polySegments)
        {
            return new Polygon()
            {
                Regions = SegmentChainer(polySegments.Segments),
                Inverted = polySegments.IsInverted
            };
        }
    }
}