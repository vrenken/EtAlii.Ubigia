﻿// ReSharper disable all
namespace HashLib.Crypto
{
    internal class SHA1 : SHA0
    {
        protected override void Expand(uint[] a_data)
        {
            for (var i = 16; i < 80; i++)
            {
                var T = a_data[i - 3] ^ a_data[i - 8] ^ a_data[i - 14] ^ a_data[i - 16];
                a_data[i] = ((T << 1) | (T >> 31));
            }
        }
    }
}