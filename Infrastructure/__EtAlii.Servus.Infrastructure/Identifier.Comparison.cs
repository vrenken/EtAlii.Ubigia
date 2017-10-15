namespace EtAlii.Servus.Model.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial struct Identifier
    {
        public override bool Equals(object obj)
        {
            bool equals = true;

            var identifier = (Identifier)obj;
            if (identifier.Moment != Moment)
            {
                equals = false;
            }
            else if (identifier.Period != Period)
            {
                equals = false;
            }
            else if (identifier.Root != Root)
            {
                equals = false;
            }
            else if (identifier.Account != Account)
            {
                equals = false;
            }

            return equals;
        }

        public static bool operator ==(Identifier first, Identifier second)
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

        public static bool operator !=(Identifier first, Identifier second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            return Root.GetHashCode() ^
                   ShiftAndWrap(Account.GetHashCode(), 2) ^ 
                   ShiftAndWrap(Period.GetHashCode(), 4) ^
                   ShiftAndWrap(Moment.GetHashCode(), 6);
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
