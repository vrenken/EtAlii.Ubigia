namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MenuItem
    {
        public IHostCommand Command { get; }

        public string Header { get; }
        public List<MenuItem> Items { get; } = new List<MenuItem>();

        public MenuItem(string header)
        {
            Header = header;
        }

        public MenuItem(string header, IHostCommand command)
        {
            Command = command;
            Header = header;
        }

    }
}