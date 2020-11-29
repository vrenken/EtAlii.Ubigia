namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;

    public partial class ProfilingAspect : IEquatable<ProfilingAspect>
    {

        public override bool Equals(object obj)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // Cannot happen.
            // Optimization for a common success case. 
            //if (Object.ReferenceEquals(this, obj))
            //[
            //    return true
            //]

            // If run-time types are not exactly the same, return false. 
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((ProfilingAspect)obj);
        }

        public bool Equals(ProfilingAspect other)
        {
            // Cannot happen. 
            // If parameter is null, return false. 
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            // Cannot happen.
            // Optimization for a common success case. 
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Can happen, but is not problematic.
            // If run-time types are not exactly the same, return false. 
            if (GetType() != other.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (other.Layer != Layer)
            {
                return false;
            }

            if (other.Id != Id)
            {
                return false;
            }

            return true;
        }

        public static bool operator ==(ProfilingAspect first, ProfilingAspect second)
        {
            var equals = false;
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

        public static bool operator !=(ProfilingAspect first, ProfilingAspect second)
        {
            var equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            return Layer.GetHashCode() ^
                   ShiftAndWrap(Id.GetHashCode(), 2);
        }

        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            var number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded. 
            var wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits. 
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

        #endregion Hashing

    }
}