// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Threading
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public partial class SingleElementOrderablePartitioner<T> : OrderablePartitioner<T>
    {
        // The collection being wrapped by this Partitioner
        private readonly IEnumerable<T> _referenceEnumerable;

        // Constructor just grabs the collection to wrap
        public SingleElementOrderablePartitioner(IEnumerable<T> enumerable)
            : base(true, true, true)
        {
            // Verify that the source IEnumerable is not null

            _referenceEnumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
        }

        // Produces a list of "numPartitions" IEnumerators that can each be
        // used to traverse the underlying collection in a thread-safe manner.
        // This will return a static number of enumerators, as opposed to
        // GetOrderableDynamicPartitions(), the result of which can be used to produce
        // any number of enumerators.
        public override IList<IEnumerator<KeyValuePair<long, T>>> GetOrderablePartitions(int partitionCount)
        {
            if (partitionCount < 1) throw new ArgumentOutOfRangeException(nameof(partitionCount));

            var list = new List<IEnumerator<KeyValuePair<long, T>>>(partitionCount);

            // Since we are doing static partitioning, create an InternalEnumerable with reference
            // counting of spawned InternalEnumerators turned on.  Once all of the spawned enumerators
            // are disposed, dynamicPartitions will be disposed.
            using var dynamicPartitions = new InternalEnumerable(_referenceEnumerable.GetEnumerator(), true);
            for (var i = 0; i < partitionCount; i++) 
            {
                list.Add(dynamicPartitions.GetEnumerator());
            }

            return list;
        }

        // Returns an instance of our internal Enumerable class.  GetEnumerator()
        // can then be called on that (multiple times) to produce shared enumerators.
        public override IEnumerable<KeyValuePair<long, T>> GetOrderableDynamicPartitions()
        {
            // Since we are doing dynamic partitioning, create an InternalEnumerable with reference
            // counting of spawned InternalEnumerators turned off.  This returned InternalEnumerable
            // will need to be explicitly disposed.
            return new InternalEnumerable(_referenceEnumerable.GetEnumerator(), false);
        }

        // Must be set to true if GetDynamicPartitions() is supported.
        public override bool SupportsDynamicPartitions => true;
    }
}