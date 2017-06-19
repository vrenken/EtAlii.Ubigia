namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Text;

    public static partial class Base36Convert
    {
                                        // "000000000011111111112222222222333333";
                                        // "012345678901234567890123456789012345";
        private const string _characters = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static UInt64 ToUInt64(String base36String)
        {
            base36String = base36String.ToLower();
            UInt64 result = 0;

            var i = 0;
            var length = base36String.Length;
            
            while (i < length)
            {
                var characterValue = _characters.IndexOf(base36String[i]);
                result = (result * 36) + (UInt32)characterValue;
                i++;
            }

            return result;
        }

        public static String ToString(UInt64 uInt64)
        {
            var builder = new StringBuilder();
            do
            {
                var remainder = (uInt64%36);
                uInt64 = (uInt64 - remainder) / 36;
                var c = _characters[(Int32)remainder];
                builder.Insert(0, c);
            } while (uInt64 > 0);

            return builder.ToString();
        }
    }
}