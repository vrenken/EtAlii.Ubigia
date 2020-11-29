namespace EtAlii.Ubigia
{
    using System;

    public partial struct Relation
    {

        public override bool Equals(object obj)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // If run-time types are not exactly the same, return false. 
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Relation)obj);
        }

        public bool Equals(Relation other)
        {
            // If run-time types are not exactly the same, return false. 
            if (GetType() != other.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (other.Id != Id)
            {
                return false;
            }

            if (other.Moment != Moment)
            {
                return false;
            }
            
            return true;
        }

        public static bool operator ==(Relation first, Relation second)
        {
            var equals = first.Equals(second);
            return equals;
        }

        public static bool operator !=(Relation first, Relation second)
        {
            var equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^
                   ShiftAndWrap(Moment.GetHashCode(), 2);
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
