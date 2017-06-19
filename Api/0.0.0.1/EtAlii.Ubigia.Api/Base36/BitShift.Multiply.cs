namespace EtAlii.Ubigia.Api.Fabric
{
    using System;

    internal static partial class BitShift
    {
        public static void Multiply(ref bool[] target, bool[] multiplication)
        {
            //Dump("M: {0}", multiplication);
            //Dump("T: {0}", target);

            var original = new bool[target.Length];
            Buffer.BlockCopy(target, 0, original, 0, target.Length);

            target = new bool[] { };

            var bitsToIterate = multiplication.Length;
            for (int i = bitsToIterate - 1; i >= 0; i--)
            {
                if (multiplication[i])
                {
                    Add(ref target, original);
                    //Dump("A: {0}", original);
                    //Dump("R: {0}", result);
                }
                var newOriginal = new bool[original.Length + 1];
                Buffer.BlockCopy(original, 0, newOriginal, 0, original.Length);
                original = newOriginal;
            }

            //Dump("R: {0}", result);
        }
    }
}