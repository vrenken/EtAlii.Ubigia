namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Workflow;
    using Serilog;

    public class TaskBasedCommandProcessor : CommandProcessor, IDisposable
    {
        private readonly ILogger _logger = Log.ForContext<ICommandProcessor>();
        private readonly IJournalViewModel _journal;
        private readonly ConcurrentQueue<Tuple<ICommand, ICommandHandler>> _queue;
        private readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private readonly ManualResetEvent _stoppedEvent = new ManualResetEvent(false);
        private readonly AutoResetEvent _enqueuedEvent = new AutoResetEvent(false);
        private readonly WaitHandle[] _events;

        public TaskBasedCommandProcessor(
            IJournalViewModel journal)
        {
            _journal = journal;
            _queue = new ConcurrentQueue<Tuple<ICommand, ICommandHandler>>();

            _events = new WaitHandle[]
            {
                _enqueuedEvent,
                _stopEvent,
            };
            Task.Factory.StartNew(Dequeue);
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
