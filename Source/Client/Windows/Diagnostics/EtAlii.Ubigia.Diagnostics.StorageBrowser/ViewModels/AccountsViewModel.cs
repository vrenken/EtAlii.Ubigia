namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Windows.Mvvm;
    using Serilog;

    public class AccountsViewModel : BindableBase, IAccountsViewModel
    {
        public IEnumerable<Account> AvailableAccounts { get => _availableAccounts;
            private set => SetProperty(ref _availableAccounts, value);
        }
        private IEnumerable<Account> _availableAccounts;

        protected IManagementConnection Connection { get; }

        public ICommand AddCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ClearCommand { get; }

        public Account SelectedAccount { get => _selectedAccount; set => SetProperty(ref _selectedAccount, value); }
        private Account _selectedAccount;
        public const string SelectedAccountProperty = "SelectedAccount";

        public string AccountName { get => _accountName; set => SetProperty(ref _accountName, value); }
        private string _accountName;
        public const string AccountNameProperty = "AccountName";

        public string AccountPassword { get => _accountPassword; set => SetProperty(ref _accountPassword, value); }
        private string _accountPassword;

        #pragma warning disable S2068 // False positives. The below isn't a password.
        private const string _accountPasswordProperty = "AccountPassword";
        #pragma warning restore S2068 

        public AccountTemplate[] AvailableAccountTemplates => AccountTemplate.All;

        public AccountTemplate SelectedAccountTemplate { get => _selectedAccountTemplate; set => SetProperty(ref _selectedAccountTemplate, value); }
        private AccountTemplate _selectedAccountTemplate;

        private readonly ILogger _logger = Log.ForContext<IAccountsViewModel>();

        public AccountsViewModel(IManagementConnection connection)
        {
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
            result &= !string.IsNullOrWhiteSpace(AccountName);
            result &= !string.IsNullOrWhiteSpace(AccountPassword);
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
                    await Connection.Accounts.Add(AccountName, AccountPassword, SelectedAccountTemplate).ConfigureAwait(false);
                });
                task.Wait();
            }
            catch
            {
                // TODO: [TO_REACTIVEUI] Rewrite this tool to ReactiveUI. This should make these kind of patterns easier to handle.
            }
            finally
            {
                ReloadAvailableAccounts();
            }
        }

        private bool CanSaveAccount(object sender)
        {
            var result = SelectedAccount != null;
            result &= !string.IsNullOrWhiteSpace(AccountName);
            result &= !string.IsNullOrWhiteSpace(AccountPassword);
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
                    await Connection.Accounts.Change(SelectedAccount.Id, AccountName, AccountPassword).ConfigureAwait(false);
                });
                task.Wait();
            }
            catch
            {
                // TODO: [TO_REACTIVEUI] Rewrite this tool to ReactiveUI. This should make these kind of patterns easier to handle.
            }
            finally
            {
                ReloadAvailableAccounts();
            }
        }

        private bool CanDeleteAccount(object sender)
        {
            var result = SelectedAccount != null;
            result &= !string.IsNullOrWhiteSpace(AccountName);
            result &= !string.IsNullOrWhiteSpace(AccountPassword);
            result &= SelectedAccountTemplate == null;
            return result;
        }

        private void DeleteAccount(object sender)
        {
            try
            {
                _logger.Verbose("Removing account: {0}", SelectedAccount.Name);

                var task = Task.Run(async () => { await Connection.Accounts.Remove(SelectedAccount.Id).ConfigureAwait(false); });
                task.Wait();
            }
            catch
            {
                // TODO: [TO_REACTIVEUI] Rewrite this tool to ReactiveUI. This should make these kind of patterns easier to handle.
            }
            finally
            {
                ReloadAvailableAccounts();
            }
        }

        private bool CanClearAccount(object sender)
        {
            var result = SelectedAccount != null;
            result |= !string.IsNullOrWhiteSpace(AccountName);
            result |= !string.IsNullOrWhiteSpace(AccountPassword);
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
                    AccountName = SelectedAccount != null ? SelectedAccount.Name : string.Empty;
                    AccountPassword = SelectedAccount != null ? SelectedAccount.Password : string.Empty;
                    SelectedAccountTemplate = null;
                    break;
                case AccountNameProperty:
                    SelectedAccountTemplate = null;
                    break;
                case _accountPasswordProperty:
                    SelectedAccountTemplate = null;
                    break;
            }
        }

        private void ReloadAvailableAccounts()
        {
            _logger.Information("Reloading accounts");

            IEnumerable<Account> accounts = null;
            var task = Task.Run(async () =>
            {
                accounts = await Connection.Accounts
                    .GetAll()
                    .ToArrayAsync()
                    .ConfigureAwait(false);
            });
            task.Wait();

            AvailableAccounts = accounts;
            ClearSelection();
        }

        private void ClearSelection()
        {
            SelectedAccount = null;
            AccountName = string.Empty;
            AccountPassword = string.Empty;
        }

    }
}
