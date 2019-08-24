namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Windows.Mvvm;

    internal class ItemChecker : BindableBase, IItemChecker
    {
        //private readonly ILogger _logger
        private readonly IItemUpdater _itemUpdater;
        private readonly ConcurrentQueue<ItemCheckAction> _queue = new ConcurrentQueue<ItemCheckAction>();
        private readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private readonly AutoResetEvent _enqueuedEvent = new AutoResetEvent(false);
        private readonly WaitHandle[] _events;

        public FolderSyncConfiguration Configuration { get => _configuration; set => SetProperty(ref _configuration, value); }
        private FolderSyncConfiguration _configuration;

        public bool IsRunning { get => _isRunning; private set => SetProperty(ref _isRunning, value); }
        private bool _isRunning;

        public ItemChecker(
            //ILogger logger,
            IItemUpdater itemUpdater)
        {
            //_logger = logger
            _itemUpdater = itemUpdater;
            _events = new WaitHandle[]
            {
                _enqueuedEvent,
                _stopEvent,
            };

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Configuration":
                    _itemUpdater.Configuration = Configuration;
                    break;
            }
        }


        public void Start()
        {
            if (!IsRunning)
            {
                Task.Run(async () => await Dequeue());
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                _stopEvent.Set();
            }
        }

        public void Enqueue(ItemCheckAction action)
        {
            _queue.Enqueue(action);
            _enqueuedEvent.Set();
        }

        private async Task Dequeue()
        {
            IsRunning = true;

            while (true)
            {
                var eventId = WaitHandle.WaitAny(_events);
                var evt = _events[eventId];
                if (evt == _stopEvent)
                {
                    break;
                }

                if (evt != _enqueuedEvent) continue;

                while (_queue.TryDequeue(out var action))
                {
                    await _itemUpdater.Update(action);
                }
            }
            IsRunning = false;
        }
    }
}
