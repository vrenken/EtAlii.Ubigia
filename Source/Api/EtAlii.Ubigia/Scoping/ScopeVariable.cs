// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    /// <summary>
    /// A ScopeVariable instance is used to cache the value of a variable in the scope of a script.
    /// </summary>
    public sealed class ScopeVariable
    {
        /// <summary>
        /// The current value of the variable within the scope of the script.
        /// </summary>
        public IObservable<object> Value { get; }

        /// <summary>
        /// The source of the variable. I.e. what script action created the value and assigned it to a variable?
        /// </summary>
        public string Source { get; }

        public ScopeVariable(object value, string source)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = Observable.Create<object>(observer =>
            {
                IEnumerable enumerable;// = new object[] []
                if (value is string || !(value is IEnumerable))
                {
                    enumerable = new[] { value };
                }
                else
                {
                    enumerable = (IEnumerable)value;
                }

                foreach (var item in enumerable)
                {
                    observer.OnNext(item);
                }
                observer.OnCompleted();
                return Disposable.Empty;
            }).ToHotObservable();

            Source = source ?? string.Empty;
        }

        public ScopeVariable(IObservable<object> value, string source)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Source = source ?? string.Empty;
        }
    }
}
