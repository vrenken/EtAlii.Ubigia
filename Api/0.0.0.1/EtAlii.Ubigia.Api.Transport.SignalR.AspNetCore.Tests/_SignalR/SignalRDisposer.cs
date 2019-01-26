namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
    using System;
    using System.Threading;

    /// <summary>
    /// Helper class to manage disposing a resource at an arbirtary time
    /// 
    /// </summary>
    internal class SignalRDisposer : IDisposable
    {
        private static readonly object _disposedSentinel = new object();
        private object _disposable;

        public void Set(IDisposable disposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));
            object obj = Interlocked.CompareExchange(ref _disposable, disposable, null);
            if (obj == null || obj != _disposedSentinel)
                return;
            disposable.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            if (!(Interlocked.Exchange<object>(ref _disposable, _disposedSentinel) is IDisposable disposable))
                return;
            disposable.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SignalRDisposer()
        {
            Dispose(false);
        }
    }
}
