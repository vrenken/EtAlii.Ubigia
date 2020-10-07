namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using EtAlii.Ubigia.Windows.Mvvm;
    using EtAlii.xTechnology.Diagnostics;

    internal class FolderMonitor : BindableBase, IFolderMonitor
    {
        private readonly ILogger _logger;
        private readonly FileSystemWatcher _watcher;
        private readonly IItemChecker _itemChecker;

        public FolderSyncConfiguration Configuration { get => _configuration; set => SetProperty(ref _configuration, value); }
        private FolderSyncConfiguration _configuration;
        
        public event EventHandler Changed = delegate { };
        public event EventHandler Error = delegate { };

        public bool IsRunning { get => _isRunning; private set => SetProperty(ref _isRunning, value); }
        private bool _isRunning;

        public bool HasError { get => _hasError; set => SetProperty(ref _hasError, value); }
        private bool _hasError;
    
        public FolderMonitor(
            ILogger logger, 
            IItemChecker itemChecker)
        {
            _logger = logger;
            _itemChecker = itemChecker;
            _watcher = new FileSystemWatcher();

            _watcher.IncludeSubdirectories = true;
            _watcher.NotifyFilter =
                NotifyFilters.Size |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName |
                NotifyFilters.DirectoryName |
                NotifyFilters.CreationTime;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Configuration":
                    _itemChecker.Configuration = Configuration;
                    break;
            }
        }

        public void Start()
        {
            try
            {
                _logger.Info("Starting folder monitoring");
                IsRunning = false;

                if (_configuration == null)
                {
                    throw new InvalidOperationException("A FolderMonitor instance cannot be started without having a configuration assigned.");
                }
                if (string.IsNullOrWhiteSpace(Configuration.LocalFolder))
                {
                    throw new InvalidOperationException("A FolderMonitor instance cannot be started without having a folder assigned.");
                }
                if (string.IsNullOrWhiteSpace(Configuration.RemoteName))
                {
                    throw new InvalidOperationException("A FolderMonitor instance cannot be started without having a remote location assigned.");
                }

                Directory.CreateDirectory(Configuration.LocalFolder);
                _watcher.Path = Configuration.LocalFolder;

                _itemChecker.Start();

                StartWatching();
                _watcher.EnableRaisingEvents = true;

                IsRunning = true;
                _logger.Info("Started folder monitoring");
            }
            catch (Exception e)
            {
                _logger.Critical("Unable to start folder monitoring", e);
                IsRunning = false;
                HasError = true;
            }
        }

        private void StartWatching()
        {
            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.Error += OnError;
        }

        private void StopWatching()
        {
            _watcher.Changed -= OnChanged;
            _watcher.Created -= OnCreated;
            _watcher.Deleted -= OnDeleted;
            _watcher.Renamed -= OnRenamed;
            _watcher.Error -= OnError;
        }

        public void Stop()
        {
            try
            {
                _logger.Info("Stopping folder monitoring");

                StopWatching();
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();

                _itemChecker.Stop();

                IsRunning = false;
                _logger.Info("Stopped folder monitoring");
            }
            catch (Exception e)
            {
                _logger.Critical("Unable to stop folder monitoring", e);
                IsRunning = false;
                HasError = true;
            }
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            _logger.Info("Error in folder monitoring");
            Error(this, e);

            //Stop()
            //Start()
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _itemChecker.Enqueue(new ItemCheckAction { Item = e.FullPath, Change = ItemChange.Destroyed });
            Changed(this, e);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _itemChecker.Enqueue(new ItemCheckAction { Item = e.FullPath, Change = ItemChange.Created });
            Changed(this, e);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _itemChecker.Enqueue(new ItemCheckAction { Item = e.FullPath, Change = ItemChange.Changed });
            Changed(this, e);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _itemChecker.Enqueue(new ItemCheckAction { Item = e.FullPath, OldItem = e.OldFullPath, Change = ItemChange.Changed });
            Changed(this, e);
        }
    }
}
