namespace EtAlii.Ubigia.Windows.Client
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Shows the change storage dialog.
    /// </summary>
    public class CloseDialogCommand : CommandBase<CloseDialogCommand>
    {
        public override void Execute(object parameter)
        {
            var window = (Window)parameter;
            window.DialogResult = true;
            window.Close();

            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}