namespace EtAlii.Ubigia
{
    using System;

    internal static partial class BitShift
    {
        public static void Multiply(ref Span<bool> target, bool[] multiplication)
        {
            Span<bool> original = new bool[target.Length];
            target.CopyTo(original);

            target = new bool[] { };

            var bitsToIterate = multiplication.Length;
            for (var i = bitsToIterate - 1; i >= 0; i--)
            {
                if (multiplication[i])
                {
                    Add(ref target, original);
                }
                Span<bool> newOriginal = new bool[original.Length + 1];
                original.CopyTo(newOriginal);
                original = newOriginal;
            }
        }
    }
}