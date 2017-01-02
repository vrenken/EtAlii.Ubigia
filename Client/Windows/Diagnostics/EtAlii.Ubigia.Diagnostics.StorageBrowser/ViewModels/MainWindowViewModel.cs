namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public StoragesViewModel Storages { get { return _storages; } }
        private readonly StoragesViewModel _storages;

        public AccountsViewModel Accounts { get { return _accounts; } }
        private readonly AccountsViewModel _accounts;

        public SpacesViewModel Spaces { get { return _spaces; } }
        private readonly SpacesViewModel _spaces;

        public RolesViewModel Roles { get { return _roles; } }
        private readonly RolesViewModel _roles;

        protected IManagementConnection Connection { get { return _connection; } }
        private readonly IManagementConnection _connection;

        private readonly ILogger _logger;

        public MainWindowViewModel(
            IManagementConnection connection, 
            StoragesViewModel storagesViewModel, 
            AccountsViewModel accountsViewModel, 
            SpacesViewModel spacesViewModel, 
            RolesViewModel rolesViewModel,
            ILogger logger)
        {
            _logger = logger;   
            _connection = connection;
            _storages = storagesViewModel;
            _accounts = accountsViewModel;
            _spaces = spacesViewModel;
            _roles = rolesViewModel;

            Accounts.PropertyChanged += OnAccountsPropertyChanged; 
        }

        void OnAccountsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case AccountsViewModel.SelectedAccountProperty:
                    Spaces.SelectedAccount = Accounts.SelectedAccount;
                    Roles.SelectedAccount = Accounts.SelectedAccount;
                    break;
            }
        }
    }
}
