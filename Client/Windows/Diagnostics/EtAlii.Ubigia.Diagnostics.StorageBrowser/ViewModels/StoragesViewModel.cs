namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class StoragesViewModel : BindableBase, IStoragesViewModel
    {
        public IEnumerable<Storage> AvailableStorages { get { return _availableStorages; } private set { SetProperty(ref _availableStorages, value); } }
        private IEnumerable<Storage> _availableStorages;

        protected IManagementConnection Connection { get; }

        public ICommand AddCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ClearCommand { get; }

        public Storage SelectedStorage { get { return _selectedStorage; } set { SetProperty(ref _selectedStorage, value); } }
        private Storage _selectedStorage;
        public const string SelectedStorageProperty = "SelectedStorage";

        public string StorageName { get { return _storageName; } set { SetProperty(ref _storageName, value); } }
        private string _storageName;

        public string StorageAddress { get { return _storageAddress; } set { SetProperty(ref _storageAddress, value); } }
        private string _storageAddress;

        private readonly ILogger _logger;

        public StoragesViewModel(IManagementConnection connection, ILogger logger)
        {
            _logger = logger;   
            Connection = connection;
            AddCommand = new RelayCommand(AddStorage, CanAddStorage);
            SaveCommand = new RelayCommand(SaveStorage, CanSaveStorage);
            DeleteCommand = new RelayCommand(DeleteStorage, CanDeleteStorage);
            ClearCommand = new RelayCommand(ClearStorage, CanClearStorage);

            ReloadAvailableStorages();
        }

        private bool CanAddStorage(object sender)
        {
            var result = SelectedStorage == null;
            result &= !String.IsNullOrWhiteSpace(StorageName);
            result &= !String.IsNullOrWhiteSpace(StorageAddress);
            result &= Uri.IsWellFormedUriString(StorageAddress, UriKind.RelativeOrAbsolute);
            return result;
        }

        private void AddStorage(object sender)
        {
            try
            {
                _logger.Verbose("Adding storage: {0}", StorageName);

                var task = Task.Run(async () =>
                {
                    await Connection.Storages.Add(StorageName, StorageAddress);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableStorages();
            }
        }

        private bool CanSaveStorage(object sender)
        {
            var result = SelectedStorage != null;
            result &= !String.IsNullOrWhiteSpace(StorageName);
            result &= !String.IsNullOrWhiteSpace(StorageAddress);
            result &= Uri.IsWellFormedUriString(StorageAddress, UriKind.RelativeOrAbsolute);
            if (SelectedStorage != null)
            {
                result &= SelectedStorage.Name != StorageName || SelectedStorage.Address != StorageAddress;
            }
            return result;
        }

        private void SaveStorage(object sender)
        {
            try
            {
                _logger.Verbose("Saving storage: {0}", StorageName);

                var task = Task.Run(async () =>
                {
                    await Connection.Storages.Change(SelectedStorage.Id, StorageName, StorageAddress);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableStorages();
            }
        }

        private bool CanDeleteStorage(object sender)
        {
            var result = SelectedStorage != null;
            result &= !String.IsNullOrWhiteSpace(StorageName);
            result &= !String.IsNullOrWhiteSpace(StorageAddress);
            return result;
        }

        private void DeleteStorage(object sender)
        {
            try
            {
                _logger.Verbose("Removing storage: {0}", SelectedStorage.Name);

                var task = Task.Run(async () =>
                {
                    await Connection.Storages.Remove(SelectedStorage.Id);
                });
                task.Wait();
            }
            catch { }
            finally
            {
                ReloadAvailableStorages();
            }
        }

        private bool CanClearStorage(object sender)
        {
            var result = SelectedStorage != null;
            result |= !String.IsNullOrWhiteSpace(StorageName);
            result |= !String.IsNullOrWhiteSpace(StorageAddress);
            return result;
        }

        private void ClearStorage(object sender)
        {
            ClearSelection();
        }

        protected override void NotifyPropertyChanged(object sender, object oldValue, object newValue, string propertyName = null)
        {
            base.NotifyPropertyChanged(sender, oldValue, newValue, propertyName);

            switch (propertyName)
            {
                case SelectedStorageProperty:
                    StorageName = SelectedStorage != null ? SelectedStorage.Name : String.Empty;
                    StorageAddress = SelectedStorage != null ? SelectedStorage.Address : String.Empty;
                    break;
            }
        }

        private void ReloadAvailableStorages()
        {
            _logger.Info("Reloading storages");

            IEnumerable<Storage> storages = null;
            var task = Task.Run(async () =>
            {
                storages = await Connection.Storages.GetAll();
            });
            task.Wait();

            AvailableStorages = storages;
            ClearSelection();
        }

        private void ClearSelection()
        {
            SelectedStorage = null;
            StorageName = String.Empty;
            StorageAddress = String.Empty;
        }

    }
}
