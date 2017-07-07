namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Reactive.Linq;

    public static class IObservableSubscribeWithNextExtension
    {

        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action onFirst, Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            observable.FirstOrDefaultAsync().Subscribe((o) => onFirst());
            return observable.Subscribe(onNext, onError, onCompleted);
        }
    }
}
