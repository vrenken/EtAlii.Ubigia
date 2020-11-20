namespace EtAlii.Ubigia.Windows.Client
{
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Windows.Settings;

    /// <summary>
    /// Shows the change storage dialog.
    /// </summary>
    public class RemoveStorageCommand : CommandBase<RemoveStorageCommand>
    {
        public override void Execute(object parameter)
        {
            var caption = $"{Shared.App.StorageNaming} removal";
            var question = string.Format("Are you sure to remove the {0}? This action cannot be undone an will result in the deletion of all data contained in the {0}.", Shared.App.StorageNaming);

            var result = MessageBox.Show(question, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                var window = (StorageWindow)parameter;
                var viewModel = window.DataContext;
                var globalSettings = Container.GetInstance<IGlobalSettings>();
                globalSettings.Storage.Remove(viewModel.StorageSettings);
                window.Close();
            }

            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}