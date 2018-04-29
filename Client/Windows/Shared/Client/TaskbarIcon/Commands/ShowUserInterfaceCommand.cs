using System.Windows.Input;

namespace EtAlii.Ubigia.Windows.Client
{
    /// <summary>
    /// Shows some part of the user interface.
    /// </summary>
    public class ShowUserInterfaceCommand : CommandBase<ShowUserInterfaceCommand>
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

            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}