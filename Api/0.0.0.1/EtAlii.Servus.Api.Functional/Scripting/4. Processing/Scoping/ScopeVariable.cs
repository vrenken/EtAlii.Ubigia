namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Collections;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    /// <summary>
    /// A ScopeVariable instance is used to cache the value of a variable in the scope of a script.
    /// </summary>
    public class ScopeVariable
    {
        /// <summary>
        /// The current value of the variable within the scope of the script.
        /// </summary>
        public IObservable<object> Value { get; private set; }
        
        /// <summary>
        /// The source of the variable. I.e. what script action created the value and assigned it to a variable?
        /// </summary>
        public string Source { get; private set; }

        public ScopeVariable(object value, string source)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Value = Observable.Create<object>(observer =>
            {
                IEnumerable enumerable = new object[] {};
                if (value is string || (value is IEnumerable) == false)
                {
                    enumerable = new object[] { value };
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

            Source = source ?? String.Empty;
        }

        public ScopeVariable(IObservable<object> value, string source)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Value = value;
            Source = source ?? String.Empty;
        }
    }
}
