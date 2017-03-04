namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport;
    using Space = EtAlii.Ubigia.Api.Space;

    public class SpacesViewModel : BindableBase, ISpacesViewModel
    {
        public IEnumerable<Space> AvailableSpaces { get { return _availableSpaces; } private set { SetProperty(ref _availableSpaces, value); } }
        private IEnumerable<Space> _availableSpaces;

        protected IManagementConnection Connection => _connection;
        private readonly IManagementConnection _connection;

        public ICommand AddCommand => _addCommand;
        private readonly ICommand _addCommand;

        public ICommand SaveCommand => _saveCommand;
        private readonly ICommand _saveCommand;

        public ICommand DeleteCommand => _deleteCommand;
        private readonly ICommand _deleteCommand;

        public ICommand ClearCommand => _clearCommand;
        private readonly ICommand _clearCommand;

        public Space SelectedSpace { get { return _selectedSpace; } set { SetProperty(ref _selectedSpace, value); } }
        private Space _selectedSpace;
        public const string SelectedSpaceProperty = "SelectedSpace";

        public Account SelectedAccount { get { return _selectedAccount; } set { SetProperty(ref _selectedAccount, value); } }
        private Account _selectedAccount;
        public const string SelectedAccountProperty = "SelectedAccount";

        public string SpaceName { get { return _spaceName; } set { SetProperty(ref _spaceName, value); } }
        private string _spaceName;
        public const string SpaceNameProperty = "SpaceName";

        public SpaceTemplate[] AvailableSpaceTemplates => SpaceTemplate.All;

        public SpaceTemplate SelectedSpaceTemplate { get { return _selectedSpaceTemplate; } set { SetProperty(ref _selectedSpaceTemplate, value); } }
        private SpaceTemplate _selectedSpaceTemplate;

        private readonly ILogger _logger;

        public SpacesViewModel(IManagementConnection connection, ILogger logger)
        {
            _logger = logger;   
            _connection = connection;
            _addCommand = new RelayCommand(AddSpace, CanAddSpace);
            _saveCommand = new RelayCommand(SaveSpace, CanSaveSpace);
            _deleteCommand = new RelayCommand(DeleteSpace, CanDeleteSpace);
            _clearCommand = new RelayCommand(ClearSpace, CanClearSpace);

            ReloadAvailableSpaces();
        }

        private bool CanAddSpace(object sender)
        {
            var result = SelectedSpace == null;
            result &= SelectedAccount != null;
            result &= SelectedSpaceTemplate != null;
            result &= !String.IsNullOrWhiteSpace(SpaceName);
            return result;
        }

        private void AddSpace(object sender)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    await Connection.Spaces.Add(SelectedAccount.Id, SpaceName, SelectedSpaceTemplate);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableSpaces();
            }
        }

        private bool CanSaveSpace(object sender)
        {
            var result = SelectedSpace != null;
            result &= SelectedAccount != null;
            result &= !String.IsNullOrWhiteSpace(SpaceName);
            result &= SelectedSpaceTemplate == null;
            if (SelectedSpace != null)
            {
                result &= SelectedSpace.Name != SpaceName;
            }
            return result;
        }

        private void SaveSpace(object sender)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    await Connection.Spaces.Change(SelectedSpace.Id, SpaceName);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableSpaces();
            }
        }

        private bool CanDeleteSpace(object sender)
        {
            var result = SelectedSpace != null;
            result &= SelectedAccount != null;
            result &= SelectedSpaceTemplate == null;
            result &= !String.IsNullOrWhiteSpace(SpaceName);
            return result;
        }

        private void DeleteSpace(object sender)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    await Connection.Spaces.Remove(SelectedSpace.Id);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableSpaces();
            }
        }

        private bool CanClearSpace(object sender)
        {
            var result = SelectedSpace != null;
            result |= !String.IsNullOrWhiteSpace(SpaceName);
            return result;
        }

        private void ClearSpace(object sender)
        {
            ClearSelection();
        }

        protected override void NotifyPropertyChanged(object sender, object oldValue, object newValue, string propertyName = null)
        {
            base.NotifyPropertyChanged(sender, oldValue, newValue, propertyName);

            switch (propertyName)
            {
                case SelectedSpaceProperty:
                    SpaceName = SelectedSpace != null ? SelectedSpace.Name : String.Empty;
                    SelectedSpaceTemplate = null;
                    break;
                case SelectedAccountProperty:
                    ReloadAvailableSpaces();
                    break;
                case SpaceNameProperty:
                    SelectedSpaceTemplate = null;
                    break;
            }
        }

        private void ReloadAvailableSpaces()
        {
            IEnumerable<Space> spaces = null;
            var task = Task.Run(async () =>
            {
                spaces = SelectedAccount != null ? await Connection.Spaces.GetAll(SelectedAccount.Id) : new Space[] { };
            });
            task.Wait();

            AvailableSpaces = spaces;
            ClearSelection();
        }

        private void ClearSelection()
        {
            SelectedSpace = null;
            SpaceName = String.Empty;
        }

    }
}
