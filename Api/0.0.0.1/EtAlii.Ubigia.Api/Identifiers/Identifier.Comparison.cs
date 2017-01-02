namespace EtAlii.Ubigia.Api
{
    using System;

    public partial struct Identifier : IEquatable<Identifier>, IEditableIdentifier
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
            //{
            //    return true;
            //}

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Identifier)obj);
        }

        public bool Equals(Identifier id)
        {
            // Cannot happen. 
            // If parameter is null, return false. 
            //if (Object.ReferenceEquals(id, null))
            //{
            //    return false;
            //}

            // Cannot happen.
            // Optimization for a common success case. 
            //if (Object.ReferenceEquals(this, id))
            //{
            //    return true;
            //}

            // Can happen, but is not problematic.
            // If run-time types are not exactly the same, return false. 
            //if (this.GetType() != id.GetType())
            //{
            //    return false;
            //}

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (id.Moment != Moment)
            {
                return false;
            }

            if (id.Period != Period)
            {
                return false;
            }

            if (id.Era != Era)
            {
                return false;
            }

            if (id.Space != Space)
            {
                return false;
            }

            if (id.Account != Account)
            {
                return false;
            }

            if (id.Storage != Storage)
            {
                return false;
            }
            
            return true;
        }

        public static bool operator ==(Identifier first, Identifier second)
        {
            bool equals = false;
            if ((object)first != null && (object)second != null)
            {
                equals = first.Equals(second);
            }

            // Cannot happen.
            //else if ((object)first == null && (object)second == null)
            //{
            //    equals = true;
            //}
            return equals;
        }

        public static bool operator !=(Identifier first, Identifier second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            return this.Storage.GetHashCode() ^
                   ShiftAndWrap(this.Account.GetHashCode(), 2) ^
                   ShiftAndWrap(this.Space.GetHashCode(), 4) ^
                   ShiftAndWrap(this.Period.GetHashCode(), 6) ^
                   ShiftAndWrap(this.Moment.GetHashCode(), 8);
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
