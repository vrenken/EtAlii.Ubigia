﻿// ReSharper disable all

using System.Diagnostics;

namespace HashLib.Hash32
{
    internal class FNV1a : Hash, IHash32, IBlockHash
    {
        private uint m_hash;

        public FNV1a()
            : base(4, 1)
        {
        }

        public override void Initialize()
        {
            m_hash = 2166136261;
        }

        public override void TransformBytes(byte[] a_data, int a_index, int a_length)
        {
            Debug.Assert(a_index >= 0);
            Debug.Assert(a_length >= 0);
            Debug.Assert(a_index + a_length <= a_data.Length);

            for (var i = a_index; a_length > 0; i++, a_length--)
                m_hash = (m_hash ^ a_data[i]) * 16777619;
        }

        public override HashResult TransformFinal()
        {
            return new HashResult(m_hash);
        }
    }
}
