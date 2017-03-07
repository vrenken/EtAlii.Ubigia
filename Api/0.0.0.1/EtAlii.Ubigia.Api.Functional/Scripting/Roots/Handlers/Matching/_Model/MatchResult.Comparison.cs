namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal partial class MatchResult : IEquatable<MatchResult>
    {
        public override bool Equals(object obj)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            //if (Object.ReferenceEquals(this, obj))
            //{
            //    return true;
            //}

            // If run-time types are not exactly the same, return false. 
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((MatchResult)obj);
        }

        public bool Equals(MatchResult match)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(match, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            //if (Object.ReferenceEquals(this, match))
            //{
            //    return true;
            //}

            // Can happen, but is not problematic.
            // If run-time types are not exactly the same, return false. 
            //if (this.GetType() != match.GetType())
            //{
            //    return false;
            //}

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (match.RootHandler != RootHandler)
            {
                return false;
            }


            if (match.Match.Length != Match.Length)
            {
                return false;
            }

            if (match.Rest.Length != Rest.Length)
            {
                return false;
            }

            for (int i = 0; i < Match.Length; i++)
            {
                if (Match[i].ToString() != match.Match[i].ToString())
                {
                    return false;
                }
            }

            for (int i = 0; i < Rest.Length; i++)
            {
                if (Rest[i].ToString() != match.Rest[i].ToString())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(MatchResult first, MatchResult second)
        {
            bool equals = false;
            if ((object)first != null && (object)second != null)
            {
                equals = first.Equals(second);
            }
            else if ((object)first == null && (object)second == null)
            {
                equals = true;
            }
            return equals;
        }

        public static bool operator !=(MatchResult first, MatchResult second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            int result = 0;

            for (int i = 0; i < Match.Length; i++)
            {
                var pathSubjectPart = Match[i];
                var power = 2 ^ i;
                result ^= ShiftAndWrap(pathSubjectPart.GetHashCode(), power);
            }

            for (int i = 0; i < Match.Length; i++)
            {
                var pathSubjectPart = Rest[i];
                var power = 2 ^ i;
                result ^= ShiftAndWrap(pathSubjectPart.GetHashCode(), power);
            }

            return result;
        }

        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded. 
            uint wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits. 
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

        #endregion Hashing
    }
}