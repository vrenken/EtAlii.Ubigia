namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Provisioning;
    using EtAlii.xTechnology.Logging;

    public class TrayIconHost : ProviderHostBase, ITrayIconProviderHost
    {
        public bool IsRunning { get { return _isRunning; } set { SetProperty(ref _isRunning, value); } }
        private bool _isRunning;

        public bool HasError { get { return _hasError; } set { SetProperty(ref _hasError, value); } }
        private bool _hasError;

        public ITaskbarIcon TaskbarIcon => _taskbarIcon;
        private readonly ITaskbarIcon _taskbarIcon;

        private readonly ILogger _logger;

        public TrayIconHost(
            IDataContext data,
            IHostConfiguration configuration,
            IProviderManager providerManager,
            ITaskbarIcon taskbarIcon,
            ILogger logger)
            : base(data, configuration, providerManager)
        {
            _taskbarIcon = taskbarIcon;
            _logger = logger;
        }

        public override void Start()
        {
            _taskbarIcon.Dispatcher.Invoke(() =>
            {
                _taskbarIcon.Visibility = Visibility.Visible;
            });
            Task.Delay(500).ContinueWith((o) =>
            {
                try
                {
                    IsRunning = false;

                    base.Start();

                    IsRunning = true;
                }
                catch (Exception e)
                {
                    HasError = true;
                    _logger.Critical("Fatal exception in infrastructure provisioning", e);
                    _logger.Info("Restarting infrastructure provisioning");
                    Task.Delay(2000);
                    HasError = false;
                    Stop();
                    Task.Delay(1000);
                    Start();
                }
            });
        }

        public override void Stop()
        {
            IsRunning = false;
            base.Stop();
        }

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, newValue)) return false;

            var oldValue = storage;
            storage = newValue;
            this.NotifyPropertyChanged(this, storage, newValue, propertyName);

            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected virtual void NotifyPropertyChanged(object sender, object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
