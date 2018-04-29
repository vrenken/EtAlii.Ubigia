namespace EtAlii.Ubigia.Windows.Management
{
    using System;

    internal partial class ConnectionDialogViewModel
    {
        private bool CanSaveAndClose(object parameter)
        {
            var result = false;

            var window = parameter as ConnectionDialogWindow;
            if (window != null)
            {
                var passwordBox = window.PasswordBox;
                result = !String.IsNullOrEmpty(passwordBox.Password) &&
                            !String.IsNullOrEmpty(Account) &&
                            !String.IsNullOrEmpty(Address) &&
                            IsTested;
            }
            return result;
        }

        private void SaveAndClose(object parameter)
        {
            var window = parameter as ConnectionDialogWindow;
            if (window != null)
            {
                var passwordBox = window.PasswordBox;
                _connectionSettingsPersister.Save(passwordBox.Password);
                window.DialogResult = true;
            }
        }
    }
}
