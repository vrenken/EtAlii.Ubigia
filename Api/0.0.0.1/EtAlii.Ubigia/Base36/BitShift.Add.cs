namespace EtAlii.Ubigia.Api.Fabric
{
    using System;

    internal static partial class BitShift
    {
        public static void Add(ref bool[] target, bool[] addition)
        {
            long carry = 0;

            if (target.Length < addition.Length)
            {
                var bitsToIterate = target.Length;
                var delta = addition.Length - bitsToIterate;
                for (int i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (target[i] ? 1 : 0) + (addition[i + delta] ? 1 : 0);
                    target[i] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }

                bitsToIterate = delta;
                for (int i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (addition[i] ? 1 : 0);
                    var newTarget = new bool[target.Length + 1];
                    Buffer.BlockCopy(target, 0, newTarget, 1, target.Length);
                    target = newTarget;
                    target[0] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }
            }
            else
            {
                var bitsToIterate = addition.Length;
                var delta = target.Length - bitsToIterate;
                for (int i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (target[i + delta] ? 1 : 0) + (addition[i] ? 1 : 0);
                    target[i + delta] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }

                bitsToIterate = delta;
                for (int i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (target[i] ? 1 : 0);
                    target[i] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }
            }
            while (carry > 0)
            {
                var newTarget = new bool[target.Length + 1];
                Buffer.BlockCopy(target, 0, newTarget, 1, target.Length);
                target = newTarget;
                target[0] = true;
                carry >>= 1;
            }
        }
    }
}