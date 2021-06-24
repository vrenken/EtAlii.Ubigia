// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Reactive.Linq;

    public static class ObservableToHotObservableExtension
    {
        public static IObservable<T> ToHotObservable<T>(this IObservable<T> observable)
        {
            var hotObservable = observable.Replay();
            hotObservable.Connect();
            return hotObservable.AsObservable();
        }
    }
}
