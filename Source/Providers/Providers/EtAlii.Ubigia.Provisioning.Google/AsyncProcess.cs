// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class AsyncProcess
    {
        private Task _task;
        private readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private readonly WaitHandle[] _events;

        protected TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1);

        public event Action<Exception> Error; 

        private static readonly object LockObject = new object(); 

        protected AsyncProcess()
        {
            _events = new WaitHandle[]
            {
                _stopEvent
            };
        }

        public Task Start()
        {
            if (_task != null)
            {
                throw new NotSupportedException($"The {GetType().Name} has already been started.");
            }
            _task = Task.Factory.StartNew(RunInternal);
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            if (_task == null)
            {
                throw new NotSupportedException($"The {GetType().Name} has not yet been started.");
            }
            _stopEvent.Set();
            _task.Wait();
            _task = null;
            return Task.CompletedTask;
        }

        private void RunInternal()
        {
            while (true)
            {
                try
                {
                    lock (LockObject)
                    {
                        var task = Run();
                        task.Wait();
                    }
                }
                catch (Exception e)
                {
                    Error?.Invoke(e);
                }

                // See if we need to iterate once more.
                var eventId = WaitHandle.WaitAny(_events, Interval);
                var evt = eventId < _events.Length && eventId >= 0 ? _events[eventId] : null;
                if (evt == _stopEvent)
                {
                    break;
                }
            }
        }

        protected abstract Task Run();
    }
}