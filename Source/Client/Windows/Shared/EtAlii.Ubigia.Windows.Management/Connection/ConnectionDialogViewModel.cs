﻿namespace EtAlii.Ubigia.Windows.Management
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Windows.Mvvm;

    internal partial class ConnectionDialogViewModel : BindableBase
    {
        public IEnumerable<ConnectionSettings> PreviousSettings { get => _previousSettings; set => SetProperty(ref _previousSettings, value); }
        private IEnumerable<ConnectionSettings> _previousSettings;

        public ConnectionSettings CurrentSettings { get => _currentSettings; set => SetProperty(ref _currentSettings, value); }
        private ConnectionSettings _currentSettings;

        public string Address { get => _address; set => SetProperty(ref _address, value); }
        private string _address;

        public string Account { get => _account; set => SetProperty(ref _account, value); }
        private string _account;

        public bool IsTested { get => _isTested; set => SetProperty(ref _isTested, value); }
        private bool _isTested;

        public bool RememberPassword { get => _rememberPassword; set => SetProperty(ref _rememberPassword, value); }
        private bool _rememberPassword;

        public TransportType Transport{ get => _transport; set => SetProperty(ref _transport, value); }
        private TransportType _transport = TransportType.Grpc;

        public ICommand SaveAndCloseCommand { get; }

        public ICommand TestCommand { get; }

        public ICommand CancelCommand { get; }

        public readonly string ConfigurationFileName = "Connection.config";

        private readonly ConnectionSettingsPersister _connectionSettingsPersister;
        private readonly ConnectionDialogWindow _window;
        
        public bool ShowTransportSelection => Debugger.IsAttached;

        public ConnectionDialogViewModel(ConnectionDialogWindow window, string defaultServer, string defaultLogin, string defaultPassword)
        {
            _window = window;
            string password;
            _connectionSettingsPersister = new ConnectionSettingsPersister(this);
            _connectionSettingsPersister.Load(out password);

            if (Debugger.IsAttached)
            {
                defaultServer = "http://localhost:64001/admin";
                defaultLogin = "Administrator";
                defaultPassword = password = "administrator123";
            }

            SetDefaults(defaultServer, defaultLogin, defaultPassword, password);

            SaveAndCloseCommand = new RelayCommand(SaveAndClose, CanSaveAndClose);
            TestCommand = new RelayCommand(Test, CanTest);
            CancelCommand = new RelayCommand(Cancel, CanCancel);

            PropertyChanged += OnPropertyChanged;
        }

        private void SetDefaults(string defaultAddress, string defaultAccount, string defaultPassword, string password)
        {
            if (string.IsNullOrWhiteSpace(_window.PasswordBox.Password) &&
                string.IsNullOrWhiteSpace(Address) &&
                string.IsNullOrWhiteSpace(Account))
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
                case nameof(Transport):
                case nameof(Address):
                case nameof(Account):
                    IsTested = false;
                    break;
                case nameof(CurrentSettings):
                    if (CurrentSettings != null)
                    {
                        Address = CurrentSettings.Address;
                        Account = CurrentSettings.Account;
                        Transport = (TransportType) Enum.Parse(typeof(TransportType), CurrentSettings.TransportType);
                        _window.PasswordBox.Password = CurrentSettings.Password;
                        IsTested =
                            !string.IsNullOrWhiteSpace(Address) &&
                            !string.IsNullOrWhiteSpace(Account) &&
                            !string.IsNullOrWhiteSpace(_window.PasswordBox.Password);
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