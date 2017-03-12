namespace EtAlii.xTechnology.Threading
{
    using System.Collections.Concurrent;

    public partial class SingleElementOrderablePartitioner<T> : OrderablePartitioner<T>
    {
        // Class used to wrap m_index for the purpose of sharing access to it
        // between an InternalEnumerable and multiple InternalEnumerators
        private class Shared<TValue>
        {
            internal TValue Value;

            public Shared(TValue item)
            {
                Value = item;
            }
        }
    }
}