// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.ObjectModel;

    public class MenuItemViewModel : BindableBase
    {
        public System.Windows.Input.ICommand Command { get; }

        public string Header { get; }
        public ObservableCollection<MenuItemViewModel> Items { get; } = new();

        public MenuItemViewModel(string header)
        {
            Header = header;
        }

        public MenuItemViewModel(string header, System.Windows.Input.ICommand command)
        {
            Command = command;
            Header = header;
        }

    }
}
