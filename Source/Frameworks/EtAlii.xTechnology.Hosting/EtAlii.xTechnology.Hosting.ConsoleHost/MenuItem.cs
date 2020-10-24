namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public ICommand Command { get; }

        public string Header { get; }
        public List<MenuItem> Items { get; } = new List<MenuItem>();

        public MenuItem(string header)
        {
            Header = header;
        }

        public MenuItem(string header, ICommand command)
        {
            Command = command;
            Header = header;
        }

    }
}