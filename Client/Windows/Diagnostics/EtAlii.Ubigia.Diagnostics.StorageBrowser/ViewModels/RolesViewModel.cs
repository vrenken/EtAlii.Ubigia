namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class RolesViewModel : BindableBase, IRolesViewModel
    {
        public IEnumerable<Role> AvailableRoles { get { return _availableRoles; } private set { SetProperty(ref _availableRoles, value); } }
        private IEnumerable<Role> _availableRoles;

        protected IManagementConnection Connection { get; }

        public ICommand AddCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ClearCommand { get; }

        public Role SelectedRole { get { return _selectedRole; } set { SetProperty(ref _selectedRole, value); } }
        private Role _selectedRole;
        public const string SelectedRoleProperty = "SelectedRole";

        public Account SelectedAccount { get { return _selectedAccount; } set { SetProperty(ref _selectedAccount, value); } }
        private Account _selectedAccount;
        public const string SelectedAccountProperty = "SelectedAccount";

        public string RoleName { get { return _roleName; } set { SetProperty(ref _roleName, value); } }
        private string _roleName;

        private readonly ILogger _logger;

        public RolesViewModel(IManagementConnection connection, ILogger logger)
        {
            _logger = logger;   
            Connection = connection;
            AddCommand = new RelayCommand(AddRole, CanAddRole);
            SaveCommand = new RelayCommand(SaveRole, CanSaveRole);
            DeleteCommand = new RelayCommand(DeleteRole, CanDeleteRole);
            ClearCommand = new RelayCommand(ClearRole, CanClearRole);

            ReloadAvailableRoles();
        }

        private bool CanAddRole(object sender)
        {
            var result = SelectedRole == null;
            result &= SelectedAccount != null;
            result &= !String.IsNullOrWhiteSpace(RoleName);
            return result;
        }

        private void AddRole(object sender)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    SelectedAccount.Roles = SelectedAccount.Roles
                        .Union(new[] {RoleName})
                        .ToArray();
                    
                    await Connection.Accounts.Change(SelectedAccount);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableRoles();
            }
        }

        private bool CanSaveRole(object sender)
        {
            var result = SelectedRole != null;
            result &= SelectedAccount != null;
            result &= !String.IsNullOrWhiteSpace(RoleName);
            if (SelectedRole != null)
            {
                result &= SelectedRole.Name != RoleName;
            }
            return result;
        }

        private void SaveRole(object sender)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    SelectedAccount.Roles = SelectedAccount.Roles
                        .Except(new[] { SelectedRole.Name })
                        .Union(new[] { RoleName })
                        .ToArray();
                    await Connection.Accounts.Change(SelectedAccount);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableRoles();
            }
        }

        private bool CanDeleteRole(object sender)
        {
            var result = SelectedRole != null;
            result &= SelectedAccount != null;
            result &= !String.IsNullOrWhiteSpace(RoleName);
            return result;
        }

        private void DeleteRole(object sender)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    SelectedAccount.Roles = SelectedAccount.Roles
                        .Except(new[] { SelectedRole.Name })
                        .ToArray();
                    await Connection.Accounts.Change(SelectedAccount);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableRoles();
            }
        }

        private bool CanClearRole(object sender)
        {
            var result = SelectedRole != null;
            result |= !String.IsNullOrWhiteSpace(RoleName);
            return result;
        }

        private void ClearRole(object sender)
        {
            ClearSelection();
        }

        protected override void NotifyPropertyChanged(object sender, object oldValue, object newValue, string propertyName = null)
        {
            base.NotifyPropertyChanged(sender, oldValue, newValue, propertyName);

            switch (propertyName)
            {
                case SelectedRoleProperty:
                    RoleName = SelectedRole != null ? SelectedRole.Name : String.Empty;
                    break;
                case SelectedAccountProperty:
                    ReloadAvailableRoles();
                    break;
            }
        }

        private void ReloadAvailableRoles()
        {
            IEnumerable<Role> roles = new Role[] { };
            var task = Task.Run(async () =>
            {
                if (SelectedAccount != null)
                {
                    var account = await Connection.Accounts.Get(SelectedAccount.Id);
                    roles = account.Roles.Select(r => new Role {Name = r});
                }
            });
            task.Wait();

            AvailableRoles = roles;
            ClearSelection();
        }

        private void ClearSelection()
        {
            SelectedRole = null;
            RoleName = String.Empty;
        }

    }
}
