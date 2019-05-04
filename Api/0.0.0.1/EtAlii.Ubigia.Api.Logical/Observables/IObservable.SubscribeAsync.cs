﻿namespace EtAlii.Ubigia.Api.Logical
{
    using System;
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
            return onError != null 
                ? source.Subscribe(OnNext, onError) 
                : source.Subscribe(OnNext); 
        }
    }
}