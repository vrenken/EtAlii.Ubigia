namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Reactive.Linq;

    public static class ObservableSubscribeOnFirstExtension 
    {
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action onFirst, Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            observable.FirstOrDefaultAsync().Subscribe((o) => onFirst());
            return observable.Subscribe(onNext, onError, onCompleted);
        }
    }
}