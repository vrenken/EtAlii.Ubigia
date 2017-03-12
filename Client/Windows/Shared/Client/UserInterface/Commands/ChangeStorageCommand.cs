using EtAlii.Ubigia.Client.Windows.Shared;
using System.Windows.Input;

namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    /// <summary>
    /// Shows the change storage dialog.
    /// </summary>
    public class ChangeStorageCommand : CommandBase<ChangeStorageCommand>
    {
        public override void Execute(object parameter)
        {
            var window = App.Current.MainWindow;

            var storageWindow = Container.GetInstance<StorageWindow>();
            storageWindow.Owner = window;
            storageWindow.DataContext.StorageSettings = parameter as StorageSettings;
            var result = storageWindow.ShowDialog();
            if (result == true)
            {

            }

            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}