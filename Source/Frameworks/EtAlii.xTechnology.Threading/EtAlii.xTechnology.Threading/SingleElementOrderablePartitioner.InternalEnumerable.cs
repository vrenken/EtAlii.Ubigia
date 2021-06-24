// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Threading
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public partial class SingleElementOrderablePartitioner<T>
    {
        // Internal class that serves as a shared enumerable for the
        // underlying collection.
        private sealed class InternalEnumerable : IEnumerable<KeyValuePair<long, T>>, IDisposable
        {
            private readonly IEnumerator<T> _reader;
            private bool _disposed;
            private readonly Shared<long> _index;

            // These two are used to implement Dispose() when static partitioning is being performed
            private int _activeEnumerators;
            private readonly bool _downcountEnumerators;

            // "downcountEnumerators" will be true for static partitioning, false for
            // dynamic partitioning.
            public InternalEnumerable(IEnumerator<T> reader, bool downcountEnumerators)
            {
                _reader = reader;
                _index = new Shared<long>(0);
                _activeEnumerators = 0;
                _downcountEnumerators = downcountEnumerators;
            }

            public IEnumerator<KeyValuePair<long, T>> GetEnumerator()
            {
                if (_disposed)
                    throw new ObjectDisposedException("InternalEnumerable: Can't call GetEnumerator() after disposing");

                // For static partitioning, keep track of the number of active enumerators.
                if (_downcountEnumerators) Interlocked.Increment(ref _activeEnumerators);

                return new InternalEnumerator(_reader, this, _index);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }


            private void Dispose(bool disposing)
            {
                if (disposing && !_disposed)
                {
                    // Only dispose the source enumerator if you are doing dynamic partitioning
                    if (!_downcountEnumerators)
                    {
                        _reader.Dispose();
                    }

                    _disposed = true;
                }
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~InternalEnumerable()
            {
                Dispose(false);
            }

            // Called from Dispose() method of spawned InternalEnumerator.  During
            // static partitioning, the source enumerator will be automatically
            // disposed once all requested InternalEnumerators have been disposed.
            public void DisposeEnumerator()
            {
                if (_downcountEnumerators && Interlocked.Decrement(ref _activeEnumerators) == 0)
                {
                    _reader.Dispose();
                }
            }
        }
    }
}
