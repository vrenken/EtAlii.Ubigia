namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class AccountsViewModel : BindableBase, IAccountsViewModel
    {
        public IEnumerable<Account> AvailableAccounts { get { return _availableAccounts; } private set { SetProperty(ref _availableAccounts, value); } }
        private IEnumerable<Account> _availableAccounts;

        protected IManagementConnection Connection { get; }

        public ICommand AddCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ClearCommand { get; }

        public Account SelectedAccount { get { return _selectedAccount; } set { SetProperty(ref _selectedAccount, value); } }
        private Account _selectedAccount;
        public const string SelectedAccountProperty = "SelectedAccount";

        public string AccountName { get { return _accountName; } set { SetProperty(ref _accountName, value); } }
        private string _accountName;
        public const string AccountNameProperty = "AccountName";

        public string AccountPassword { get { return _accountPassword; } set { SetProperty(ref _accountPassword, value); } }
        private string _accountPassword;
        public const string AccountPasswordProperty = "AccountPassword";

        public AccountTemplate[] AvailableAccountTemplates => AccountTemplate.All;

        public AccountTemplate SelectedAccountTemplate { get { return _selectedAccountTemplate; } set { SetProperty(ref _selectedAccountTemplate, value); } }
        private AccountTemplate _selectedAccountTemplate;

        private readonly ILogger _logger;

        public AccountsViewModel(IManagementConnection connection, ILogger logger)
        {
            _logger = logger;   
            Connection = connection;
            AddCommand = new RelayCommand(AddAccount, CanAddAccount);
            SaveCommand = new RelayCommand(SaveAccount, CanSaveAccount);
            DeleteCommand = new RelayCommand(DeleteAccount, CanDeleteAccount);
            ClearCommand = new RelayCommand(ClearAccount, CanClearAccount);

            ReloadAvailableAccounts();
        }

        private bool CanAddAccount(object sender)
        {
            var result = SelectedAccount == null;
            result &= !String.IsNullOrWhiteSpace(AccountName);
            result &= !String.IsNullOrWhiteSpace(AccountPassword);
            result &= SelectedAccountTemplate != null;
            return result;
        }

        private void AddAccount(object sender)
        {
            try
            {
                _logger.Verbose("Adding account: {0}", AccountName);

                var task = Task.Run(async () =>
                {
                    await Connection.Accounts.Add(AccountName, AccountPassword, SelectedAccountTemplate);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableAccounts();
            }
        }

        private bool CanSaveAccount(object sender)
        {
            var result = SelectedAccount != null;
            result &= !String.IsNullOrWhiteSpace(AccountName);
            result &= !String.IsNullOrWhiteSpace(AccountPassword);
            result &= SelectedAccountTemplate == null;
            if (SelectedAccount != null)
            {
                result &= SelectedAccount.Name != AccountName || SelectedAccount.Password != AccountPassword;
            }
            return result;
        }

        private void SaveAccount(object sender)
        {
            try
            {
                _logger.Verbose("Saving account: {0}", AccountName);

                var task = Task.Run(async () =>
                {
                    await Connection.Accounts.Change(SelectedAccount.Id, AccountName, AccountPassword);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableAccounts();
            }
        }

        private bool CanDeleteAccount(object sender)
        {
            var result = SelectedAccount != null;
            result &= !String.IsNullOrWhiteSpace(AccountName);
            result &= !String.IsNullOrWhiteSpace(AccountPassword);
            result &= SelectedAccountTemplate == null;
            return result;
        }

        private void DeleteAccount(object sender)
        {
            try
            {
                _logger.Verbose("Removing account: {0}", SelectedAccount.Name);

                var task = Task.Run(async () =>
                {
                    await Connection.Accounts.Remove(SelectedAccount.Id);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableAccounts();
            }
        }

        private bool CanClearAccount(object sender)
        {
            var result = SelectedAccount != null;
            result |= !String.IsNullOrWhiteSpace(AccountName);
            result |= !String.IsNullOrWhiteSpace(AccountPassword);
            return result;
        }

        private void ClearAccount(object sender)
        {
            ClearSelection();
        }

        protected override void NotifyPropertyChanged(object sender, object oldValue, object newValue, string propertyName = null)
        {
            base.NotifyPropertyChanged(sender, oldValue, newValue, propertyName);

            switch (propertyName)
            {
                case SelectedAccountProperty:
                    AccountName = SelectedAccount != null ? SelectedAccount.Name : String.Empty;
                    AccountPassword = SelectedAccount != null ? SelectedAccount.Password : String.Empty;
                    SelectedAccountTemplate = null;
                    break;
                case AccountNameProperty:
                    SelectedAccountTemplate = null;
                    break;
                case AccountPasswordProperty:
                    SelectedAccountTemplate = null;
                    break;
            }
        }

        private void ReloadAvailableAccounts()
        {
            _logger.Info("Reloading accounts");

            IEnumerable<Account> accounts = null;
            var task = Task.Run(async () =>
            {
                accounts = await Connection.Accounts.GetAll();
            });
            task.Wait();

            AvailableAccounts = accounts;
            ClearSelection();
        }

        private void ClearSelection()
        {
            SelectedAccount = null;
            AccountName = String.Empty;
            AccountPassword = String.Empty;
        }

    }
}
