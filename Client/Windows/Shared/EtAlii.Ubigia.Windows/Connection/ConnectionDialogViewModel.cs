namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.xTechnology.Mvvm;

    internal partial class ConnectionDialogViewModel : BindableBase
    {
        public IEnumerable<ConnectionSettings> PreviousSettings { get { return _previousSettings; } set { SetProperty(ref _previousSettings, value); } }
        private IEnumerable<ConnectionSettings> _previousSettings;

        public ConnectionSettings CurrentSettings { get { return _currentSettings; } set { SetProperty(ref _currentSettings, value); } }
        private ConnectionSettings _currentSettings;

        public string Address { get { return _address; } set { SetProperty(ref _address, value); } }
        private string _address;

        public string Account { get { return _account; } set { SetProperty(ref _account, value); } }
        private string _account;

        public string Space { get { return _space; } set { SetProperty(ref _space, value); } }
        private string _space;

        public bool IsTested { get { return _isTested; } set { SetProperty(ref _isTested, value); } }
        private bool _isTested;

        public bool RememberPassword { get { return _rememberPassword; } set { SetProperty(ref _rememberPassword, value); } }
        private bool _rememberPassword;

        public ICommand SaveAndCloseCommand { get; }

        public ICommand TestCommand { get; }

        public ICommand CancelCommand { get; }

        public readonly string ConfigurationFileName = "Connection.config";

        private readonly ConnectionSettingsPersister _connectionSettingsPersister;
        private readonly ConnectionDialogWindow _window;

        public TransportType Transport{ get => _transport; set => SetProperty(ref _transport, value); }
        private TransportType _transport = TransportType.Grpc;
        
        public bool ShowTransportSelection => Debugger.IsAttached;

        public ConnectionDialogViewModel(ConnectionDialogWindow window, string defaultServer, string defaultLogin, string defaultPassword, string defaultSpace)
        {
            _window = window;
            _connectionSettingsPersister = new ConnectionSettingsPersister(this);
            _connectionSettingsPersister.Load(out string password);

			if (Debugger.IsAttached)
            {
                defaultServer = "http://localhost:64000/user";
				defaultLogin = "Administrator";
				defaultPassword = password = "administrator123";
				defaultSpace = "Data";
			}

			SetDefaults(defaultServer, defaultSpace, defaultLogin, defaultPassword, password);

            SaveAndCloseCommand = new RelayCommand(SaveAndClose, CanSaveAndClose);
            TestCommand = new RelayCommand(Test, CanTest);
            CancelCommand = new RelayCommand(Cancel, CanCancel);

            PropertyChanged += OnPropertyChanged;
        }

        private void SetDefaults(string defaultAddress, string defaultSpace, string defaultAccount, string defaultPassword, string password)
        {
            if (string.IsNullOrWhiteSpace(_window.PasswordBox.Password) &&
                string.IsNullOrWhiteSpace(Address) &&
                string.IsNullOrWhiteSpace(Account) &&
                string.IsNullOrWhiteSpace(Space))
            {
                Address = defaultAddress;
                Space = defaultSpace;
                Account = defaultAccount;
                password = defaultPassword;
            }
            if (string.IsNullOrWhiteSpace(Space))
            {
                Space = "Default";
            }
            _window.PasswordBox.Password = password;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Transport):
                case nameof(Address):
                case nameof(Account):
                case nameof(Space):
                    IsTested = false;
                    break;
                case nameof(CurrentSettings):
                    if (CurrentSettings != null)
                    {
                        Address = CurrentSettings.Address;
                        Account = CurrentSettings.Account;
                        Space = CurrentSettings.Space;
                        Transport = (TransportType)Enum.Parse(typeof(TransportType), CurrentSettings.TransportType);
                        _window.PasswordBox.Password = CurrentSettings.Password;
                        IsTested =
                            !string.IsNullOrWhiteSpace(Address) &&
                            !string.IsNullOrWhiteSpace(Account) &&
                            !string.IsNullOrWhiteSpace(_window.PasswordBox.Password) &&
                            !string.IsNullOrWhiteSpace(Space);
                    }
                    break;
            }
        }

        internal void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            IsTested = false;
        }
    }
}
