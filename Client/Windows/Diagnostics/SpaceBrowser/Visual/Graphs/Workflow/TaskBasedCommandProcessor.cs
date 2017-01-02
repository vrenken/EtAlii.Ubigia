namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Workflow;
    using SimpleInjector;

    public class TaskBasedCommandProcessor : CommandProcessor, IDisposable
    {
        private readonly Container _container;
        private readonly ILogger _logger;
        private readonly IJournal _journal;
        private readonly MainDispatcherInvoker _mainDispatcherInvoker;
        private readonly ConcurrentQueue<Tuple<ICommand, ICommandHandler>> _queue;
        private readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private readonly ManualResetEvent _stoppedEvent = new ManualResetEvent(false);
        private readonly AutoResetEvent _enqueuedEvent = new AutoResetEvent(false);
        private readonly WaitHandle[] _events;

        public TaskBasedCommandProcessor(
            Container container,
            ILogger logger,
            IJournal journal,
            MainDispatcherInvoker mainDispatcherInvoker)
            : base(container)
        {
            _logger = logger;
            _container = container;
            _journal = journal;
            _mainDispatcherInvoker = mainDispatcherInvoker;
            _queue = new ConcurrentQueue<Tuple<ICommand, ICommandHandler>>();

            _events = new WaitHandle[]
            {
                _enqueuedEvent,
                _stopEvent,
            };
            Task.Factory.StartNew((Action)Dequeue);
        }

        private void Dequeue()
        {
            while (true)
            {
                var eventId = WaitHandle.WaitAny(_events);
                var evt = _events[eventId];
                if (evt == _stopEvent)
                {
                    break;
                }
                if (evt == _enqueuedEvent)
                {
                    Tuple<ICommand, ICommandHandler> action;
                    while (_queue.TryDequeue(out action))
                    {
                        var command = action.Item1;
                        var handler = action.Item2;

                        _journal.AddItem("Processing", command.ToString());
                        _logger.Verbose("Processing command: {0}", command);

                        handler.Handle(command);
                    }
                }
            }
            _stoppedEvent.Set();
        }

        protected override void ProcessCommand(ICommand command, ICommandHandler handler)
        {
            _queue.Enqueue(new Tuple<ICommand, ICommandHandler>(command, handler));
            _journal.AddItem("Queued", command.ToString());
            _logger.Verbose("Queued command: {0}", command);
            _enqueuedEvent.Set();
        }

        protected override ICommandHandler GetCommandHandler(Container container, ICommand command)
        {
            ICommandHandler handler = null;
            handler = command.GetHandler(_container);
            return handler;
        }

        #region Disposal

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TaskBasedCommandProcessor()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _stopEvent.Set();
            _stoppedEvent.WaitOne();

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
                _stopEvent.Dispose();
                _stoppedEvent.Dispose();
                _enqueuedEvent.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
        #endregion Disposal
    }
}
