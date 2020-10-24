namespace EtAlii.Ubigia.Windows.Client
{
    using System;
    using System.Windows.Input;
    using EtAlii.Ubigia.Windows.Settings;

    /// <summary>
    /// Shows the add new storage dialog.
    /// </summary>
    public class AddNewStorageCommand : CommandBase<AddNewStorageCommand>
    {
        public override void Execute(object parameter)
        {
            var window = App.Current.MainWindow;

            var storageSettings = new StorageSettings(Guid.NewGuid().ToString());
            storageSettings.Name = $"Unnamed {Shared.App.StorageNaming}";

            var storageWindow = Container.GetInstance<StorageWindow>();
            storageWindow.Owner = window;
            storageWindow.DataContext.StorageSettings = storageSettings;
            storageWindow.DataContext.StorageCanBeRemoved = false;

            var result = storageWindow.ShowDialog();
            if (result == true)
            {
                var globalSettings = Container.GetInstance<IGlobalSettings>();
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