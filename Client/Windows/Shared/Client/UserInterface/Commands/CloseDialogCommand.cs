using EtAlii.Ubigia.Client.Windows.Shared;
using System.Windows;
using System.Windows.Input;

namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    /// <summary>
    /// Shows the change storage dialog.
    /// </summary>
    public class CloseDialogCommand : CommandBase<CloseDialogCommand>
    {
        public override void Execute(object parameter)
        {
            var window = parameter as Window;
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