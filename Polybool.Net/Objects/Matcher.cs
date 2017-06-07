using System;

namespace Polybool.Net.Objects
{
    public class Matcher:IEquatable<Matcher>
    {
        public int Index { get; set; }
        public bool MatchesHead { get; set; }
        public bool MatchesPt1 { get; set; }

        public bool Equals(Matcher other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && MatchesHead == other.MatchesHead && MatchesPt1 == other.MatchesPt1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Matcher) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Index;
                hashCode = (hashCode * 397) ^ MatchesHead.GetHashCode();
                hashCode = (hashCode * 397) ^ MatchesPt1.GetHashCode();
                return hashCode;
            }
        }
    }
}