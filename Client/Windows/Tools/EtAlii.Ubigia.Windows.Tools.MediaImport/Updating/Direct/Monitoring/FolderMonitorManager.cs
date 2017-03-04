namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using Container = EtAlii.xTechnology.MicroContainer.Container;

    internal class FolderMonitorManager : BindableBase, IFolderMonitorManager
    {
        private readonly Container _container;
        private readonly ILogger _logger;

        private readonly IObservableFolderSyncConfigurationCollection _folderSyncConfigurations;

        public ObservableCollection<IFolderMonitor> Monitors => _monitors;
        private readonly ObservableCollection<IFolderMonitor> _monitors = new ObservableCollection<IFolderMonitor>();

        public bool AllMonitorsAreRunning
        {
            get { return _monitors.All(monitor => monitor.IsRunning); }
        }

        public bool IsRunning { get { return _isRunning; } set { SetProperty(ref _isRunning, value); } }
        private bool _isRunning;

        public bool HasError
        {
            get { return _monitors.All(monitor => monitor.HasError) || HasManagerError; }
        }

        public bool HasManagerError
        {
            get { return _hasManagerError; }
            set { SetProperty(ref _hasManagerError, value); }
        }

        private bool _hasManagerError;

        public FolderMonitorManager(
            Container container,
            IObservableFolderSyncConfigurationCollection folderSyncConfigurations,
            ILogger logger)
        {
            _container = container;
            _folderSyncConfigurations = folderSyncConfigurations;
            _logger = logger;


            foreach (var folderSyncConfiguration in _folderSyncConfigurations)
            {
                AddMonitor(folderSyncConfiguration);
            }
            _folderSyncConfigurations.CollectionChanged += FolderSyncConfigurationsChanged;
            _monitors.CollectionChanged += MonitorsChanged;
        }

        private void MonitorsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (IsRunning)
                    {
                        foreach (var item in e.NewItems)
                        {
                            StartMonitor((IFolderMonitor) item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (IsRunning)
                    {
                        foreach (var item in e.OldItems)
                        {
                            StopMonitor((IFolderMonitor) item);
                        }
                    }
                    break;
            }
        }

        private void FolderSyncConfigurationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        AddMonitor((FolderSyncConfiguration)item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        RemoveMonitor((FolderSyncConfiguration)item);
                    }
                    break;
            }
        }

        private void AddMonitor(FolderSyncConfiguration folderSyncConfiguration)
        {
            var monitorToAdd = _container.GetInstance<IFolderMonitor>();
            monitorToAdd.Configuration = folderSyncConfiguration;
            _monitors.Add(monitorToAdd);
        }

        private void RemoveMonitor(FolderSyncConfiguration folderSyncConfiguration)
        {
            var monitorToRemove = _monitors.Single(m => m.Configuration == folderSyncConfiguration);
            _monitors.Remove(monitorToRemove);
        }

        

        public void Start()
        {
            try
            {
                _logger.Info("Starting MediaImport");
                IsRunning = true;
                foreach (var monitor in _monitors)
                {
                    StartMonitor(monitor);
                }
                _logger.Info("Started MediaImport");
            }
            catch (Exception e)
            {
                HasManagerError = true;
                _logger.Critical("Fatal exception starting MediaImport", e);
                _logger.Info("Restarting MediaImport");
                Task.Delay(2000);
                HasManagerError = false;
                Stop();
                Start();
            }
        }

        private void StartMonitor(IFolderMonitor monitor)
        {
            monitor.PropertyChanged += OnMonitorPropertyChanged;
            monitor.Start();
        }

        public void Stop()
        {
            try
            {
                _logger.Info("Stopping MediaImport");
                IsRunning = false;
                foreach (var monitor in _monitors)
                {
                    StopMonitor(monitor);
                }

                _logger.Info("Stopped MediaImport");
            }
            catch (Exception e)
            {
                HasManagerError = true;
                _logger.Critical("Fatal exception stopping MediaImport", e);
                Task.Delay(2000);
                HasManagerError = false;
            }
        }

        private void StopMonitor(IFolderMonitor monitor)
        {
            monitor.Stop();
            monitor.PropertyChanged -= OnMonitorPropertyChanged;
        }

        private void OnMonitorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsRunning":
                    NotifyPropertyChanged(this, null, null, "AllMonitorsAreRunning");
                    break;
                case "HasManagerError":
                case "HasError":
                    NotifyPropertyChanged(this, null, null, "HasError");
                    break;
            }
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Stop();
            Start();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
        }
    }
}
