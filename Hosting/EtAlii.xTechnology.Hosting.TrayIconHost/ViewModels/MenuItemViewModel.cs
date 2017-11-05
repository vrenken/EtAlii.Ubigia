namespace EtAlii.xTechnology.Hosting
{
    using System.Windows.Input;
    using System.ComponentModel;
    using EtAlii.xTechnology.Mvvm;

    public class MenuItemViewModel : BindableBase, INotifyPropertyChanged
    {
        public ICommand Command { get; }

        public string Header { get; }
        public MenuItemViewModel[] Items { get; }

        public MenuItemViewModel(string header, MenuItemViewModel[] items = null)
        {
            Header = header;
            Items = items ?? new MenuItemViewModel[0];
        }
        public MenuItemViewModel(string header, ICommand command)
        {
            Command = command;
            Header = header;
            Items = new MenuItemViewModel[0];
        }

    }
}