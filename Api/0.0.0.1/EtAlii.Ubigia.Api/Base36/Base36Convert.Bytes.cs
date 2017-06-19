namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static partial class Base36Convert
    {
        private static readonly bool[]  _36AsBits = new[] { true, false, false, true, false, false };  

        //the "alphabet" for base-36 encoding is similar in theory to hexadecimal,
        //but uses all 26 English letters a-z instead of just a-f.
        private static readonly char[] alphabet = new[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 
                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
                'w', 'x', 'y', 'z'
            };

        public static byte[] ToBytes(string base36String)
        {
            base36String = base36String.ToLower();

            var bits = new bool[]{};

            var charactersToIterate = base36String.Length;

            for(int i = charactersToIterate - 1; i>=0; i--)
            {
                var character = base36String[i];
                var characterValue = (byte)_characters.IndexOf(character);
                var characterBits = ToBits(characterValue);
                for (int m = 0; m < charactersToIterate - i - 1; m++)
                {
                    BitShift.Multiply(ref characterBits, _36AsBits);
                }
                BitShift.Add(ref bits, characterBits);
            }

            return ToBytes(bits);
        }

        //public static byte[] ToBytes(string base36String, bool leastSignificantByteFirst = true)
        //{
        //    var bytes = new List<byte>();

        //}

        public static string ToString(byte[] bytes, bool leastSignificantByteFirst = true)
        {
            //most .NET-produced byte arrays are "little-endian" (LSB first),
            //but MSB-first is more natural to read bitwise left-to-right;
            //here, we can handle either way.
            if (leastSignificantByteFirst)
            {
                bytes = bytes.Reverse().ToArray(); 
            }

            var builder = new StringBuilder();
            while (bytes.Any(b => b > 0))
            {
                ulong mod;
                bytes = BitShift.Divide(bytes, 36, out mod);
                builder.Insert(0, alphabet[mod]);
            }

            var result = builder
                .ToString()
                .TrimStart('0');
            return String.IsNullOrEmpty(result) 
                ? "0" 
                : result;
        }

        private static void Add(ref List<bool> bits, byte value)
        {
            throw new NotImplementedException();
        }

        private static bool[] ToBits(byte value)
        {
            var result = new bool[8];

            int index = 7;
            while (value > 0)
            {
                result[index] = (value & 0x1) == 0x1;
                value >>= 0x1;
                index--;
            }

            return result;
        }

        private static byte[] ToBytes(bool[] bits)
        {
            var result = new byte[]{};

            byte currentByte = 0;            
            var bitsToIterate = bits.Length;

            byte bitCounter = 0;
            for (int i = bitsToIterate - 1; i >= 0; i--)
            {
                var currentBit = bits[i];

                currentByte |= currentBit ? (byte)(0x1 << bitCounter) : currentByte;
                bitCounter++;

                if (bitCounter == 8)
                {
                    var newResult = new byte[result.Length + 1];
                    Buffer.BlockCopy(result, 0, newResult, 1, result.Length);
                    result = newResult;
                    result[0] = currentByte;
                    currentByte = 0;
                    bitCounter = 0;
                }
            }

            if (currentByte != 0)
            {
                var newResult = new byte[result.Length + 1];
                Buffer.BlockCopy(result, 0, newResult, 1, result.Length);
                result = newResult;
                result[0] = currentByte;
            }
            return result;
        }
    }
}