namespace EtAlii.Servus.Windows.Management
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Servus.Windows;
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

        public bool IsTested { get { return _isTested; } set { SetProperty(ref _isTested, value); } }
        private bool _isTested;

        public bool RememberPassword { get { return _rememberPassword; } set { SetProperty(ref _rememberPassword, value); } }
        private bool _rememberPassword;

        public ICommand SaveAndCloseCommand { get { return _saveAndCloseCommand; } }
        private readonly ICommand _saveAndCloseCommand;

        public ICommand TestCommand { get { return _testCommand; } }
        private readonly ICommand _testCommand;

        public ICommand CancelCommand { get { return _cancelCommand; } }
        private readonly ICommand _cancelCommand;
 
        public readonly string ConfigurationFileName = "Connection.config";

        private readonly ConnectionSettingsPersister _connectionSettingsPersister;
        private readonly ConnectionDialogWindow _window;

        public ConnectionDialogViewModel(ConnectionDialogWindow window, string defaultServer, string defaultLogin, string defaultPassword)
        {
            _window = window;
            string password;
            _connectionSettingsPersister = new ConnectionSettingsPersister(this);
            _connectionSettingsPersister.Load(out password);

            SetDefaults(defaultServer, defaultLogin, defaultPassword, password);

            _saveAndCloseCommand = new RelayCommand(SaveAndClose, CanSaveAndClose);
            _testCommand = new RelayCommand(Test, CanTest);
            _cancelCommand = new RelayCommand(Cancel, CanCancel);

            this.PropertyChanged += OnPropertyChanged;
        }

        private void SetDefaults(string defaultAddress, string defaultAccount, string defaultPassword, string password)
        {
            if (String.IsNullOrWhiteSpace(_window.PasswordBox.Password) &&
                String.IsNullOrWhiteSpace(Address) &&
                String.IsNullOrWhiteSpace(Account))
            {
                Address = defaultAddress;
                Account = defaultAccount;
                password = defaultPassword;
            }
            _window.PasswordBox.Password = password;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Address":
                case "Account":
                case "Space":
                    IsTested = false;
                    break;
                case "CurrentSettings":
                    if (CurrentSettings != null)
                    {
                        Address = CurrentSettings.Address;
                        Account = CurrentSettings.Account;
                        _window.PasswordBox.Password = CurrentSettings.Password;
                        IsTested =
                            !String.IsNullOrWhiteSpace(Address) &&
                            !String.IsNullOrWhiteSpace(Account) &&
                            !String.IsNullOrWhiteSpace(_window.PasswordBox.Password);
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
