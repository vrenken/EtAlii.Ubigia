using EtAlii.Servus.Client.Windows.Shared;
using EtAlii.Servus.Client.Windows.UserInterface;
using System.Windows.Controls;
using System.Windows.Input;

namespace EtAlii.Servus.Client.Windows.TaskbarIcon
{
    /// <summary>
    /// Shows some part of the user interface.
    /// </summary>
    public abstract class ShowTabItemCommand<T> : CommandBase<T>
      where T : class, ICommand, new()
    {
        public override void Execute(object parameter)
        {
            var window = App.Current.MainWindow;

            if (window == null)
            {
                window = Container.GetInstance<MainWindow>();
                App.Current.MainWindow = window;
            }

            window.Show();
            window.BringIntoView();

            App.Current.MainWindow.TabControl.SelectedItem = TabItemToActivate;

            CommandManager.InvalidateRequerySuggested();
        }

        public abstract TabItem TabItemToActivate
        {
            get;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}