namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class ObservableSubscribeAsyncExtension 
    {
        public static IDisposable SubscribeAsync<T>(
            this IObservable<T> source, 
            Func<T,Task> onNext, 
            Action<Exception> onError = null, 
            Action onCompleted = null)
        {
            void OnNext(T o)
            {
                var task = Task.Run(() => onNext(o));
                task.Wait();
            }

            if (onError != null && onCompleted != null)
            {
                return source.Subscribe(OnNext, onError, onCompleted);
            }
            if (onCompleted != null)
            {
                return source.Subscribe(OnNext, onCompleted);                
            }
            if (onError != null)
            {
                return source.Subscribe(OnNext, onError);                
            }
            return source.Subscribe(OnNext);
        }
        
        private static IDisposable SubscribeAsyncOld<T>(
            this IObservable<T> source, 
            Func<T,Task> onNext, 
            Action<Exception> onError = null, 
            Action onCompleted = null)
        {
            async Task<T> Wrapped(T t)
            {
                await onNext(t);
                return t;
            }

            void OnNext(IObservable<T> o)
            {
                // When needed: Do something.
            }

            if (onError != null && onCompleted != null)
            {
                return source
                    .Select(o => Observable.FromAsync(async () => await Wrapped(o)))
                    .Subscribe(OnNext, onError, onCompleted);
            }

            else if (onCompleted != null)
            {
                return source
                    .Select(o => Observable.FromAsync(async () => await Wrapped(o)))
                    .Subscribe(OnNext, onCompleted);                
            }
            else if (onError != null)
            {
                return source
                    .Select(o => Observable.FromAsync(async () => await Wrapped(o)))
                    .Subscribe(OnNext, onError);                
            }
            else
            {
                return source
                    .Select(o => Observable.FromAsync(async () => await Wrapped(o)))
                    .Subscribe(OnNext);
            }
        }
    }
}