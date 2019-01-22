namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase, IMainWindowViewModel
    {
        public IStoragesViewModel Storages { get; }

        public IAccountsViewModel Accounts { get; }

        public ISpacesViewModel Spaces { get; }

        public IRolesViewModel Roles { get; }

        protected IManagementConnection Connection { get; }

        public MainWindowViewModel(
            IManagementConnection connection, 
            IStoragesViewModel storagesViewModel,
            IAccountsViewModel accountsViewModel, 
            ISpacesViewModel spacesViewModel, 
            IRolesViewModel rolesViewModel)
        {
            Connection = connection;
            Storages = storagesViewModel;
            Accounts = accountsViewModel;
            Spaces = spacesViewModel;
            Roles = rolesViewModel;

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
