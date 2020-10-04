namespace EtAlii.Ubigia
{
    using System;

    public sealed partial class ContentDefinition
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
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((ContentDefinition)obj);
        }

        public bool Equals(ContentDefinition contentDefinition)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(contentDefinition, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (ReferenceEquals(this, contentDefinition))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            //if (this.GetType() != contentDefinition.GetType())
            //{
            //    return false;
            //}

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (contentDefinition.Checksum != Checksum)
            {
                return false;
            }

            if (contentDefinition.Parts.Count != Parts.Count)
            {
                return false;
            }

            if (contentDefinition.Size != Size)
            {
                return false;
            }

            return true;
        }

        public static bool operator ==(ContentDefinition first, ContentDefinition second)
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

        public static bool operator !=(ContentDefinition first, ContentDefinition second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            return Size.GetHashCode() ^
                   ShiftAndWrap(Checksum.GetHashCode(), 2) ^
                   ShiftAndWrap(Parts.Count.GetHashCode(), 4);
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
