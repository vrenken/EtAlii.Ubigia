using EtAlii.Ubigia.Client.Windows.Shared;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace EtAlii.Ubigia.Client.Windows
{
    public class ShellExtensionService : IShellExtensionService
    {
        public void Start()
        {
            UpdateShellExtension(true);

            var globalSettings = App.Current.Container.GetInstance<IGlobalSettings>();
            StartMonitoringStorages(globalSettings.Storage);
            globalSettings.Storage.CollectionChanged += OnStoragesChanged;
        }

        public void Stop()
        {
            var globalSettings = App.Current.Container.GetInstance<IGlobalSettings>();
            globalSettings.Storage.CollectionChanged -= OnStoragesChanged;

            ShellExtension.Unregister();
            ShellExtension.ReloadWindowsExplorers();
        }
        
        void OnStoragesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    StartMonitoringStorages(e.NewItems.Cast<StorageSettings>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    StopMonitoringStorages(e.OldItems.Cast<StorageSettings>());
                    break;
            }

            UpdateShellExtension();
        }

        private void StopMonitoringStorages(IEnumerable<StorageSettings> storageSettings)
        {
            foreach (var storageSetting in storageSettings)
            {
                storageSetting.PropertyChanged -= OnStorageSettingsChanged;
            }
        }

        private void StartMonitoringStorages(IEnumerable<StorageSettings> storageSettings)
        {
            foreach (var storageSetting in storageSettings)
            {
                storageSetting.PropertyChanged += OnStorageSettingsChanged;
            }
        }

        void OnStorageSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name": 
                    UpdateShellExtension();
                    break;
            }
        }

        private void UpdateShellExtension(bool onlyTriggerOnChangedStorages = false)
        {
            var globalSettings = App.Current.Container.GetInstance<IGlobalSettings>();

            var shouldUpdate = true;
            if (onlyTriggerOnChangedStorages)
            {
                var hasMissingStorages = Registrations.GetMissing(globalSettings).Length > 0;
                var hasObsoleteStorages = Registrations.GetObsolete(globalSettings).Length > 0;
                shouldUpdate = hasMissingStorages || hasObsoleteStorages;
            }
            if (shouldUpdate || !ShellExtension.IsRegistered)
            {
                ShellExtension.Unregister();

                if(globalSettings.Storage.Count > 0)
                {
                    ShellExtension.Register();
                }
                ShellExtension.ReloadWindowsExplorers();
            }
        }
    }
}
