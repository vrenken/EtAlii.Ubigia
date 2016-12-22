using EtAlii.Servus.Client.Windows.Shared;
using System;
using System.Windows;

namespace EtAlii.Servus.Client.Windows.TaskbarIcon
{
    using EtAlii.Servus.Windows;

    public class ExitApplicationCommand : CommandBase<ExitApplicationCommand>
    {
        public override void Execute(object parameter)
        {
            var question = String.Format("Are you sure you want to close {0}? If you close {0}, some information might not be available in your system.", Constant.ProductName);
            var result = MessageBox.Show(question, Constant.ProductName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}