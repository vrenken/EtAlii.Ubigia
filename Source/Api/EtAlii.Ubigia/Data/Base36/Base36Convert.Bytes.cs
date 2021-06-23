// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Linq;
    using System.Text;

    public static partial class Base36Convert
    {
        private static readonly bool[]  _36AsBits = { true, false, false, true, false, false };

        //the "alphabet" for base-36 encoding is similar in theory to hexadecimal,
        //but uses all 26 English letters a-z instead of just a-f.
        private static readonly char[] _alphabet =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z'
        };

        public static Span<byte> ToBytes(string base36String)
        {
#if NETSTANDARD2_0
            var span = base36String.ToLower().AsSpan();
#elif NETSTANDARD2_1
            ReadOnlySpan<char> span = base36String.ToLower();
#else
            This won't work
#endif

            Span<bool> bits = Array.Empty<bool>();

            var charactersToIterate = span.Length;

            for(var i = charactersToIterate - 1; i>=0; i--)
            {
                var character = span[i];
                var characterValue = (byte)Characters.IndexOf(character);
                var characterBits = ToBits(characterValue);
                for (var m = 0; m < charactersToIterate - i - 1; m++)
                {
                    BitShift.Multiply(ref characterBits, _36AsBits);
                }
                BitShift.Add(ref bits, characterBits);
            }

            return ToBytes(bits);
        }

        public static string ToString(byte[] bytes, bool leastSignificantByteFirst = true)
        {
            //most .NET-produced byte arrays are "little-endian" (LSB first),
            //but MSB-first is more natural to read bitwise left-to-right
            //here, we can handle either way.
            if (leastSignificantByteFirst)
            {
                bytes = bytes.Reverse().ToArray();
            }

            var builder = new StringBuilder();
            while (bytes.Any(b => b > 0))
            {
                bytes = BitShift.Divide(bytes, 36, out var mod);
                builder.Insert(0, _alphabet[mod]);
            }

            var result = builder
                .ToString()
                .TrimStart('0');
            return string.IsNullOrEmpty(result)
                ? "0"
                : result;
        }

        private static Span<bool> ToBits(byte value)
        {
            var result = new bool[8];

            var index = 7;
            while (value > 0)
            {
                result[index] = (value & 0x1) == 0x1;
                value >>= 0x1;
                index--;
            }

            return result;
        }

        private static Span<byte> ToBytes(ReadOnlySpan<bool> bits)
        {
            Span<byte> result = Array.Empty<byte>();

            byte currentByte = 0;
            var bitsToIterate = bits.Length;

            byte bitCounter = 0;
            for (var i = bitsToIterate - 1; i >= 0; i--)
            {
                var currentBit = bits[i];

                currentByte |= currentBit ? (byte)(0x1 << bitCounter) : currentByte;
                bitCounter++;

                if (bitCounter == 8)
                {
                    Span<byte> newResult = new byte[result.Length + 1];
                    result.CopyTo(newResult.Slice(1));
                    result = newResult;
                    result[0] = currentByte;
                    currentByte = 0;
                    bitCounter = 0;
                }
            }

            if (currentByte != 0)
            {
                Span<byte> newResult = new byte[result.Length + 1];
                result.CopyTo(newResult.Slice(1));
                result = newResult;
                result[0] = currentByte;
            }
            return result;
        }
    }
}
