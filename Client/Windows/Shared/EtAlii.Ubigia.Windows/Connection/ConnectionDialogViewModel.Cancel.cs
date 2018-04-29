﻿namespace EtAlii.Ubigia.Windows
{
    internal partial class ConnectionDialogViewModel
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
