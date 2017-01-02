using EtAlii.Ubigia.Client.Windows.Shared;
using System;
using System.Windows.Input;

namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    /// <summary>
    /// Shows the add new storage dialog.
    /// </summary>
    public class AddNewStorageCommand : CommandBase<AddNewStorageCommand>
    {
        public override void Execute(object parameter)
        {
            var window = App.Current.MainWindow;

            var storageSettings = new StorageSettings(Guid.NewGuid().ToString());
            storageSettings.Name = String.Format("Unnamed {0}", App.StorageNaming);

            var storageWindow = Container.GetInstance<StorageWindow>();
            storageWindow.Owner = window;
            storageWindow.DataContext.StorageSettings = storageSettings;
            storageWindow.DataContext.StorageCanBeRemoved = false;

            var result = storageWindow.ShowDialog();
            if (result == true)
            {
                var globalSettings = Container.GetInstance<GlobalSettings>();
                globalSettings.Storage.Add(storageSettings);
            }
            else
            {
                storageSettings.Delete();
            }

            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}