// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.
#pragma warning disable S1144 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Query.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Storage;

    public partial class UbigiaQueryExpression
    {
        private sealed class ResultEnumerable : IEnumerable<ValueBuffer>
        {
            private readonly Func<ValueBuffer> _getElement;

            public ResultEnumerable(Func<ValueBuffer> getElement)
            {
                _getElement = getElement;
            }

            public IEnumerator<ValueBuffer> GetEnumerator()
                => new ResultEnumerator(_getElement());

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            private sealed class ResultEnumerator : IEnumerator<ValueBuffer>
            {
                private readonly ValueBuffer _value;
                private bool _moved;

                public ResultEnumerator(ValueBuffer value)
                {
                    _value = value;
                    _moved = _value.IsEmpty;
                }

                public bool MoveNext()
                {
                    if (!_moved)
                    {
                        _moved = true;

                        return _moved;
                    }

                    return false;
                }

                public void Reset()
                {
                    _moved = false;
                }

                object IEnumerator.Current
                    => Current;

                public ValueBuffer Current
                    => !_moved ? ValueBuffer.Empty : _value;

                void IDisposable.Dispose()
                {
                    // Any instances that should be disposed should be disposed in this method.
                    // As the code was copied from the EF repository currently nothing is disposed.
                }
            }
        }
    }
}