﻿using EtAlii.Ubigia.Client.Windows.Shared;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace EtAlii.Ubigia.Client.Windows
{
    public class ShellExtensionService : IApplicationService
    {
        public void Start()
        {
            UpdateShellExtension(true);

            var globalSettings = App.Current.Container.GetInstance<GlobalSettings>();
            StartMonitoringStorages(globalSettings.Storage);
            globalSettings.Storage.CollectionChanged += OnStoragesChanged;
        }

        public void Stop()
        {
            var globalSettings = App.Current.Container.GetInstance<GlobalSettings>();
            globalSettings.Storage.CollectionChanged -= OnStoragesChanged;

            EtAlii.Ubigia.Client.Windows.Shared.ShellExtension.Unregister();
            EtAlii.Ubigia.Client.Windows.Shared.ShellExtension.ReloadWindowsExplorers();
        }
        
        void OnStoragesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
            var globalSettings = App.Current.Container.GetInstance<GlobalSettings>();

            var shouldUpdate = true;
            if (onlyTriggerOnChangedStorages)
            {
                var hasMissingStorages = Registrations.GetMissing(globalSettings).Length > 0;
                var hasObsoleteStorages = Registrations.GetObsolete(globalSettings).Length > 0;
                shouldUpdate = hasMissingStorages || hasObsoleteStorages;
            }
            if (shouldUpdate || !EtAlii.Ubigia.Client.Windows.Shared.ShellExtension.IsRegistered)
            {
                EtAlii.Ubigia.Client.Windows.Shared.ShellExtension.Unregister();

                if(globalSettings.Storage.Count > 0)
                {
                    EtAlii.Ubigia.Client.Windows.Shared.ShellExtension.Register();
                }
                EtAlii.Ubigia.Client.Windows.Shared.ShellExtension.ReloadWindowsExplorers();
            }
        }
    }
}
