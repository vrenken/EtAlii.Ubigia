namespace EtAlii.Servus.Api
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

            // Optimization for a common success case. 
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Relation)obj);
        }

        public bool Equals(Relation relation)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(relation, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (ReferenceEquals(this, relation))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != relation.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (relation.Id != Id)
            {
                return false;
            }

            if (relation.Moment != Moment)
            {
                return false;
            }
            
            return true;
        }

        public static bool operator ==(Relation first, Relation second)
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

        public static bool operator !=(Relation first, Relation second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^
                   ShiftAndWrap(this.Moment.GetHashCode(), 2);
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
