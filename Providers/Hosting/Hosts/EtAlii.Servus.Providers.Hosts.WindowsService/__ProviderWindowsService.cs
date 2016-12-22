//namespace EtAlii.Servus.Provisioning.Hosting
//{
//    using EtAlii.Servus.Provisioning;
//    using EtAlii.xTechnology.Logging;
//    using System;
//    using System.ComponentModel;
//    using System.Runtime.CompilerServices;
//    using System.ServiceProcess;
//    using System.Threading.Tasks;

//    [System.ComponentModel.DesignerCategory("Code")]
//    public class ProviderWindowsService : ServiceBase
//    {
//        private readonly IHostConfiguration _configuration;
//        private readonly IComponentManager _componentManager;
//        private readonly ILogger _logger;

//        public bool IsRunning { get { return _isRunning; } set { SetProperty(ref _isRunning, value); } }
//        private bool _isRunning;

//        public bool HasError { get { return _hasError; } set { SetProperty(ref _hasError, value); } }
//        private bool _hasError;

//        public ProviderWindowsService(IHostConfiguration configuration, IComponentManager componentManager, ILogger logger)
//        {
//            _logger = logger;
//            _configuration = configuration;
//            _componentManager = componentManager;
//        }

//        protected override void OnStart(string[] args)
//        {
//            try
//            {
//                IsRunning = false;
//                _logger.Info("Starting provisioning host (service)");

//                _componentManager.Start();

//                _logger.Info("Started provisioning host (service)");
//                IsRunning = true;
//            }
//            catch (Exception e)
//            {
//                HasError = true;
//                _logger.Critical("Fatal exception in provisioning host (service)", e);
//                _logger.Info("Restarting provisioning host (service)");
//                Task.Delay(2000);
//                HasError = false;
//                Stop();
//                Start();
//            }
//        }

//        protected override void OnStop()
//        {
//            _logger.Info("Stopping provisioning host (service)");
//            IsRunning = false;

//            _componentManager.Stop();

//            _logger.Info("Stopped provisioning host (service)");

//            // End logging.
//            //Logger.EndSession(); // Disabled because of performance loss.
//        }

//        public void Start()
//        {
//            OnStart(new string[] { });
//        }

//        public new void Stop()
//        {
//            OnStop();
//        }


//        /// <summary>
//        /// Multicast event for property change notifications.
//        /// </summary>
//        public event PropertyChangedEventHandler PropertyChanged;

//        /// <summary>
//        /// Checks if a property already matches a desired value.  Sets the property and
//        /// notifies listeners only when necessary.
//        /// </summary>
//        /// <typeparam name="T">Type of the property.</typeparam>
//        /// <param name="storage">Reference to a property with both getter and setter.</param>
//        /// <param name="value">Desired value for the property.</param>
//        /// <param name="propertyName">Name of the property used to notify listeners.  This
//        /// value is optional and can be provided automatically when invoked from compilers that
//        /// support CallerMemberName.</param>
//        /// <returns>True if the value was changed, false if the existing value matched the
//        /// desired value.</returns>
//        protected bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
//        {
//            if (object.Equals(storage, newValue)) return false;

//            var oldValue = storage;
//            storage = newValue;
//            this.NotifyPropertyChanged(this, storage, newValue, propertyName);

//            return true;
//        }

//        /// <summary>
//        /// Notifies listeners that a property value has changed.
//        /// </summary>
//        /// <param name="propertyName">Name of the property used to notify listeners.  This
//        /// value is optional and can be provided automatically when invoked from compilers
//        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
//        protected virtual void NotifyPropertyChanged(object sender, object oldValue, object newValue, [CallerMemberName] string propertyName = null)
//        {
//            var eventHandler = this.PropertyChanged;
//            if (eventHandler != null)
//            {
//                eventHandler(this, new PropertyChangedEventArgs(propertyName));
//            }
//        }
//    }
//}