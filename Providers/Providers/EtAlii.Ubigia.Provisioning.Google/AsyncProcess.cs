// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class AsyncProcess
    {
        private Task _task;
        private readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private readonly WaitHandle[] _events;

        protected TimeSpan Interval {get { return _interval; } set { _interval = value; } }
        private TimeSpan _interval = TimeSpan.FromMinutes(1);

        public event Action<Exception> Error { add { _error += value; } remove { _error -= value; } }
        private Action<Exception> _error;

        private static object _lockObject = new object();

        public AsyncProcess()
        {
            _events = new WaitHandle[]
            {
                _stopEvent
            };
        }

        public void Start()
        {
            if (_task != null)
            {
                throw new NotSupportedException($"The {GetType().Name} has already been started.");
            }
            _task = Task.Factory.StartNew((Action)RunInternal);
        }

        public void Stop()
        {
            if (_task == null)
            {
                throw new NotSupportedException($"The {GetType().Name} has not yet been started.");
            }
            _stopEvent.Set();
            _task.Wait();
            _task = null;
        }

        private void RunInternal()
        {
            while (true)
            {
                try
                {
                    lock (_lockObject)
                    {
                        Run();
                    }
                }
                catch (Exception e)
                {
                    _error?.Invoke(e);
                }

                // See if we need to iterate once more.
                var eventId = WaitHandle.WaitAny(_events, _interval);
                var evt = eventId < _events.Length && eventId >= 0 ? _events[eventId] : null;
                if (evt == _stopEvent)
                {
                    break;
                }
            }
        }

        protected abstract void Run();
    }
}