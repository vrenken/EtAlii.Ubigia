﻿namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
    using System;
    using System.Threading;

    /// <summary>
    /// Helper class to manage disposing a resource at an arbirtary time
    /// 
    /// </summary>
    internal class SignalRDisposer : IDisposable
    {
        private static readonly object DisposedSentinel = new object();
        private object _disposable;

        public void Set(IDisposable disposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));
            var obj = Interlocked.CompareExchange(ref _disposable, disposable, null);
            if (obj == null || obj != DisposedSentinel)
                return;
            disposable.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            var disposable = Interlocked.Exchange<object>(ref _disposable, DisposedSentinel) as IDisposable;
            if (disposable == null)
                return;
            disposable.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
