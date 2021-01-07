namespace EtAlii.xTechnology.Threading
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public partial class SingleElementOrderablePartitioner<T>
    {
        // Internal class that serves as a shared enumerator for
        // the underlying collection.
        private sealed class InternalEnumerator : IEnumerator<KeyValuePair<long, T>>
        {
            private KeyValuePair<long, T> _current;
            private readonly IEnumerator<T> _source;
            private readonly InternalEnumerable _controllingEnumerable;
            private readonly Shared<long> _index;
            private bool _disposed;


            public InternalEnumerator(IEnumerator<T> source, InternalEnumerable controllingEnumerable, Shared<long> index)
            {
                _source = source;
                _current = default(KeyValuePair<long, T>);
                _controllingEnumerable = controllingEnumerable;
                _index = index;
            }

            object IEnumerator.Current => _current;

            KeyValuePair<long, T> IEnumerator<KeyValuePair<long, T>>.Current => _current;

            void IEnumerator.Reset()
            {
                throw new NotSupportedException("Reset() not supported");
            }

            // This method is the crux of this class.  Under lock, it calls
            // MoveNext() on the underlying enumerator, grabs Current and index,
            // and increments the index.
            bool IEnumerator.MoveNext()
            {
                var rval = false;
                lock (_source)
                {
                    rval = _source.MoveNext();
                    if (rval)
                    {
                        _current = new KeyValuePair<long, T>(_index.Value, _source.Current);
                        _index.Value = _index.Value + 1;
                    }
                    else
                    {
                        _current = default;
                    }
                }
                return rval;
            }

            private void Dispose(bool disposing)
            {
                if (disposing && !_disposed)
                {
                    // Delegate to parent enumerable's DisposeEnumerator() method
                    _controllingEnumerable.DisposeEnumerator();
                    _disposed = true;
                }
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~InternalEnumerator()
            {
                Dispose(false);
            }
        }
    }
}
