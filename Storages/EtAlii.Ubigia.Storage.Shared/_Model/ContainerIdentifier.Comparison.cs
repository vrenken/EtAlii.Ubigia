namespace EtAlii.Ubigia.Storage
{
    using System;

    public partial struct ContainerIdentifier : IEquatable<ContainerIdentifier>
    {
        public override bool Equals(object obj)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(obj, null))
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

            return this.Equals((ContainerIdentifier)obj);
        }

        public bool Equals(ContainerIdentifier id)
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
            if (id.Paths.Length != Paths.Length)
            {
                return false;
            }

            for (int i = 0; i < Paths.Length; i++)
            {
                if (id.Paths[i] != Paths[i])
                {
                    return false;
                }
            }
            
            return true;
        }

        public static bool operator ==(ContainerIdentifier first, ContainerIdentifier second)
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

        public static bool operator !=(ContainerIdentifier first, ContainerIdentifier second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            int result = 0;

            for (int i = 0; i < this.Paths.Length; i++)
            {
                var path = this.Paths[i];
                var power = 2 ^ i; 
                result ^= ShiftAndWrap(path.GetHashCode(), power);
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
