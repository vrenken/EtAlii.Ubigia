// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    internal static partial class BitShift
    {
        public static void Add(ref Span<bool> target, ReadOnlySpan<bool> addition)
        {
            long carry = 0;

            if (target.Length < addition.Length)
            {
                var bitsToIterate = target.Length;
                var delta = addition.Length - bitsToIterate;
                for (var i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (target[i] ? 1 : 0) + (addition[i + delta] ? 1 : 0);
                    target[i] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }

                bitsToIterate = delta;
                for (var i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (addition[i] ? 1 : 0);
                    Span<bool> newTarget = new bool[target.Length + 1];
                    target.CopyTo(newTarget.Slice(1));
                    target = newTarget;
                    target[0] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }
            }
            else
            {
                var bitsToIterate = addition.Length;
                var delta = target.Length - bitsToIterate;
                for (var i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (target[i + delta] ? 1 : 0) + (addition[i] ? 1 : 0);
                    target[i + delta] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }

                bitsToIterate = delta;
                for (var i = bitsToIterate - 1; i >= 0; i--)
                {
                    carry = carry + (target[i] ? 1 : 0);
                    target[i] = (carry & 0x1) == 0x1;
                    carry >>= 1;
                }
            }
            while (carry > 0)
            {
                Span<bool> newTarget = new bool[target.Length + 1];
                target.CopyTo(newTarget.Slice(1));
                target = newTarget;
                target[0] = true;
                carry >>= 1;
            }
        }
    }
}