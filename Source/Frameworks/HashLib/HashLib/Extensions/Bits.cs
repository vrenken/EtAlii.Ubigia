using System.Diagnostics;
// ReSharper disable all

namespace HashLib
{
    internal static class Bits
    {
        public static uint RotateLeft(uint a_uint, int a_n)
        {
            Debug.Assert(a_n >= 0);

            return (uint)((a_uint << a_n) | (a_uint >> (32 - a_n)));
        }

        public static uint RotateRight(uint a_uint, int a_n)
        {
            Debug.Assert(a_n >= 0);

            return (uint)((a_uint >> a_n) | (a_uint << (32 - a_n)));
        }

        public static ulong RotateRight(ulong a_ulong, int a_n)
        {
            Debug.Assert(a_n >= 0);

            return (ulong)((a_ulong >> a_n) | (a_ulong << (64 - a_n)));
        }
    }
}