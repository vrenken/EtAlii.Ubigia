namespace EtAlii.Ubigia.Windows
{
    using System;
    using EtAlii.xTechnology.Mvvm;

    internal partial class ConnectionDialogViewModel : BindableBase
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
                            !String.IsNullOrEmpty(Space) &&
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
