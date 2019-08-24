namespace EtAlii.Ubigia.Windows.Management
{
    internal partial class ConnectionDialogViewModel
    {
        private bool CanCancel(object parameter)
        {
            return parameter is ConnectionDialogWindow;
        }

        private void Cancel(object parameter)
        {
            if (parameter is ConnectionDialogWindow window)
            {
                window.DialogResult = false;
            }
        }
    }
}
