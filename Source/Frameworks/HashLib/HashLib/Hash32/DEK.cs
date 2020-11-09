// ReSharper disable all

namespace HashLib.Hash32
{
    internal class DEK : MultipleTransformNonBlock, IHash32
    {
        public DEK()
            : base(4, 1)
        {
        }

        protected override HashResult ComputeAggregatedBytes(byte[] a_data)
        {
            var hash = (uint)a_data.Length;

            foreach (var b in a_data)
                hash = ((hash << 5) ^ (hash >> 27)) ^ b;

            return new HashResult(hash);
        }
    }
}
