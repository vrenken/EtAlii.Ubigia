// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.Text;

    public static partial class Base36Convert
    {
                    // This is the number "000000000011111111112222222222333333"
                    // This is the index  "012345678901234567890123456789012345"
        private const string Characters = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static ulong ToUInt64(string base36String)
        {
            base36String = base36String.ToLower();
            ulong result = 0;

            var i = 0;
            var length = base36String.Length;

            while (i < length)
            {
                var characterValue = Characters.IndexOf(base36String[i]);
                result = (result * 36) + (uint)characterValue;
                i++;
            }

            return result;
        }

        public static string ToString(ulong uInt64)
        {
            var builder = new StringBuilder();
            do
            {
                var remainder = (uInt64%36);
                uInt64 = (uInt64 - remainder) / 36;
                var c = Characters[(int)remainder];
                builder.Insert(0, c);
            } while (uInt64 > 0);

            return builder.ToString();
        }
    }
}
