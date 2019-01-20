namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Threading.Tasks;

    // TODO: Converge with other instances.
    public static class ObservableHelper
    {
        public static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNext, Action<Exception> onError, Action onCompleted)
        {
            return source.Select(e => Observable.Defer(() => onNext(e).ToObservable())).Concat()
                .Subscribe(
                    e => { }, // empty
                    onError,
                    onCompleted);
        }
    }
}