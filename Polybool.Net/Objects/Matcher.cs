using System;

namespace Polybool.Net.Objects
{
    public class Matcher:IEquatable<Matcher>
    {
        public int index { get; set; }
        public bool matches_head { get; set; }
        public bool matches_pt1 { get; set; }

        public bool Equals(Matcher other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return index == other.index && matches_head == other.matches_head && matches_pt1 == other.matches_pt1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matcher) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = index;
                hashCode = (hashCode * 397) ^ matches_head.GetHashCode();
                hashCode = (hashCode * 397) ^ matches_pt1.GetHashCode();
                return hashCode;
            }
        }
    }
}