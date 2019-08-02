namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    /// <summary>
    /// A QueryScopeVariable instance is used to cache the value of a variable in the scope of a query.
    /// </summary>
    public class QueryScopeVariable
    {
        /// <summary>
        /// The current value of the variable within the scope of the query.
        /// </summary>
        public IObservable<object> Value { get; }
        
        /// <summary>
        /// The source of the variable. I.e. what query action created the value and assigned it to a variable?
        /// </summary>
        public string Source { get; }

        public QueryScopeVariable(object value, string source)
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

        public QueryScopeVariable(IObservable<object> value, string source)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Source = source ?? string.Empty;
        }
    }
}
