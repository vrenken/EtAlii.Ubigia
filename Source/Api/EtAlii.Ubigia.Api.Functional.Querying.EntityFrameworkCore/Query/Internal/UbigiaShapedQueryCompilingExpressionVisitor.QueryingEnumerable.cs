// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1144 // Unused private types or members should be removed

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Query.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Internal;
    using EtAlii.Ubigia.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public partial class UbigiaShapedQueryCompilingExpressionVisitor
    {
        private sealed class QueryingEnumerable<T> : IAsyncEnumerable<T>, IEnumerable<T>, IQueryingEnumerable
        {
            private readonly QueryContext _queryContext;
            private readonly IEnumerable<ValueBuffer> _innerEnumerable;
            private readonly Func<QueryContext, ValueBuffer, T> _shaper;
            private readonly Type _contextType;
            private readonly IDiagnosticsLogger<DbLoggerCategory.Query> _queryLogger;
            private readonly bool _standAloneStateManager;

            public QueryingEnumerable(
                QueryContext queryContext,
                IEnumerable<ValueBuffer> innerEnumerable,
                Func<QueryContext, ValueBuffer, T> shaper,
                Type contextType,
                bool standAloneStateManager)
            {
                _queryContext = queryContext;
                _innerEnumerable = innerEnumerable;
                _shaper = shaper;
                _contextType = contextType;
                _queryLogger = queryContext.QueryLogger;
                _standAloneStateManager = standAloneStateManager;
            }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
                => new Enumerator(this, cancellationToken);

            public IEnumerator<T> GetEnumerator()
                => new Enumerator(this);

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            public string ToQueryString()
                => UbigiaStrings.NoQueryStrings;

            private sealed class Enumerator : IEnumerator<T>, IAsyncEnumerator<T>
            {
                private IEnumerator<ValueBuffer> _enumerator;
                private readonly QueryContext _queryContext;
                private readonly IEnumerable<ValueBuffer> _innerEnumerable;
                private readonly Func<QueryContext, ValueBuffer, T> _shaper;
                private readonly Type _contextType;
                private readonly IDiagnosticsLogger<DbLoggerCategory.Query> _queryLogger;
                private readonly bool _standAloneStateManager;
                private readonly CancellationToken _cancellationToken;

                public Enumerator(QueryingEnumerable<T> queryingEnumerable, CancellationToken cancellationToken = default)
                {
                    _queryContext = queryingEnumerable._queryContext;
                    _innerEnumerable = queryingEnumerable._innerEnumerable;
                    _shaper = queryingEnumerable._shaper;
                    _contextType = queryingEnumerable._contextType;
                    _queryLogger = queryingEnumerable._queryLogger;
                    _standAloneStateManager = queryingEnumerable._standAloneStateManager;
                    _cancellationToken = cancellationToken;
                }

                public T Current { get; private set; }

                object IEnumerator.Current
                    => Current;

                public bool MoveNext()
                {
                    try
                    {
                        using (_queryContext.ConcurrencyDetector.EnterCriticalSection())
                        {
                            return MoveNextHelper();
                        }
                    }
                    catch (Exception exception)
                    {
                        _queryLogger.QueryIterationFailed(_contextType, exception);

                        throw;
                    }
                }

                public ValueTask<bool> MoveNextAsync()
                {
                    try
                    {
                        using (_queryContext.ConcurrencyDetector.EnterCriticalSection())
                        {
                            _cancellationToken.ThrowIfCancellationRequested();

                            return new ValueTask<bool>(MoveNextHelper());
                        }
                    }
                    catch (Exception exception)
                    {
                        _queryLogger.QueryIterationFailed(_contextType, exception);

                        throw;
                    }
                }

                private bool MoveNextHelper()
                {
                    if (_enumerator == null)
                    {
                        EntityFrameworkEventSource.Log.QueryExecuting();

                        _enumerator = _innerEnumerable.GetEnumerator();
                        _queryContext.InitializeStateManager(_standAloneStateManager);
                    }

                    var hasNext = _enumerator.MoveNext();

                    Current = hasNext
                        ? _shaper(_queryContext, _enumerator.Current)
                        : default;

                    return hasNext;
                }

                public void Dispose()
                {
                    _enumerator?.Dispose();
                    _enumerator = null;
                }

                public ValueTask DisposeAsync()
                {
                    var enumerator = _enumerator;
                    _enumerator = null;

                    return enumerator.DisposeAsyncIfAvailable();
                }

                public void Reset()
                    => throw new NotImplementedException();
            }
        }
    }
}
