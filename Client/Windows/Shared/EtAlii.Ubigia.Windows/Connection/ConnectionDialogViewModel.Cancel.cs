namespace EtAlii.Ubigia.Windows
{
    internal partial class ConnectionDialogViewModel
    {
        private bool CanCancel(object parameter)
        {
            return parameter is ConnectionDialogWindow window;
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
