namespace EtAlii.Servus.Windows.Management
{
    using EtAlii.xTechnology.Mvvm;

    internal partial class ConnectionDialogViewModel : BindableBase
    {
        private bool CanCancel(object parameter)
        {
            var window = parameter as ConnectionDialogWindow;
            return window != null;
        }

        private void Cancel(object parameter)
        {
            var window = parameter as ConnectionDialogWindow;
            if (window != null)
            {
                window.DialogResult = false;
            }
        }
    }
}
