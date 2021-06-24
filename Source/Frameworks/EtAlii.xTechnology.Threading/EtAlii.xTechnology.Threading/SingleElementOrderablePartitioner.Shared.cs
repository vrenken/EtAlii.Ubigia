// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Threading
{
    // ReSharper disable once UnusedTypeParameter
    public partial class SingleElementOrderablePartitioner<T>
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