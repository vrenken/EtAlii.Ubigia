namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using System.Collections.Immutable;
    using System.Threading;
    using Serilog.Context;

    // Source: https://github.com/serilog/serilog/issues/1015
    public static class ContextCorrelator
    {
        private static ImmutableDictionary<string, object> Items
        {
            get => InternalItems.Value ?? (InternalItems.Value = ImmutableDictionary<string, object>.Empty);
            set => InternalItems.Value = value;
        }
        private static readonly AsyncLocal<ImmutableDictionary<string, object>> InternalItems = new AsyncLocal<ImmutableDictionary<string, object>>();

        public static object GetValue(string key) => Items[key];

        public static IDisposable BeginCorrelationScope(string key, object value, bool throwWhenAlreadyCorrelated = true)
        {
            if (!throwWhenAlreadyCorrelated)
            {
                return BeginCorrelationScopeWhenNeeded(key, value);
            }

            if (Items.ContainsKey(key))
            {
                throw new InvalidOperationException($"{key} is already being correlated!");
            }

            var scope = new LogContextCorrelationScope(Items, LogContext.PushProperty(key, value));

            Items = Items.Add(key, value);

            return scope;
        }

        private static IDisposable BeginCorrelationScopeWhenNeeded(string key, object value)
        {
            var valueAlreadyInContext = Items.ContainsKey(key); 
            if (valueAlreadyInContext)
            {
                return new CorrelationScope(Items);
            }

            var scope = new LogContextCorrelationScope(Items, LogContext.PushProperty(key, value));
            Items = Items.Add(key, value);
            return scope;
        }

        /// <summary>
        /// A correlation scope that has nothing to do with a LogContext.
        /// </summary>
        private sealed class CorrelationScope : IDisposable
        {
            private readonly ImmutableDictionary<string, object> _bookmark;

            public CorrelationScope(ImmutableDictionary<string, object> bookmark)
            {
                _bookmark = bookmark ?? throw new ArgumentNullException(nameof(bookmark));
            }

            public void Dispose()
            {
                Items = _bookmark;
            }
        }
        /// <summary>
        /// A correlation scope that takes into account a disposable LogContext.
        /// </summary>
        private sealed class LogContextCorrelationScope : IDisposable
        {
            private readonly ImmutableDictionary<string, object> _bookmark;
            private readonly IDisposable _logContext;

            public LogContextCorrelationScope(ImmutableDictionary<string, object> bookmark, IDisposable logContext)
            {
                _bookmark = bookmark ?? throw new ArgumentNullException(nameof(bookmark));
                _logContext = logContext ?? throw new ArgumentNullException(nameof(logContext));
            }

            public void Dispose()
            {
                _logContext.Dispose();
                Items = _bookmark;
            }
        }

    }
}