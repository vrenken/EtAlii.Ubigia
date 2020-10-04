namespace EtAlii.Ubigia.Api.Fabric
{
    using System;

    internal static partial class BitShift
    {
        public static void Multiply(ref bool[] target, bool[] multiplication)
        {
            var original = new bool[target.Length];
            Buffer.BlockCopy(target, 0, original, 0, target.Length);

            target = new bool[] { };

            var bitsToIterate = multiplication.Length;
            for (var i = bitsToIterate - 1; i >= 0; i--)
            {
                if (multiplication[i])
                {
                    Add(ref target, original);
                }
                var newOriginal = new bool[original.Length + 1];
                Buffer.BlockCopy(original, 0, newOriginal, 0, original.Length);
                original = newOriginal;
            }
        }
    }
}