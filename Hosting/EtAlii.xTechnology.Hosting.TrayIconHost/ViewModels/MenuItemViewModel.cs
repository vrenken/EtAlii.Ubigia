namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using EtAlii.xTechnology.Mvvm;

    public class MenuItemViewModel : BindableBase
    {
        public ICommand Command { get; }

        public string Header { get; }
        public ObservableCollection<MenuItemViewModel> Items { get; } = new ObservableCollection<MenuItemViewModel>();

        public MenuItemViewModel(string header)
        {
            Header = header;
        }

        public MenuItemViewModel(string header, ICommand command)
        {
            Command = command;
            Header = header;
        }

    }
}