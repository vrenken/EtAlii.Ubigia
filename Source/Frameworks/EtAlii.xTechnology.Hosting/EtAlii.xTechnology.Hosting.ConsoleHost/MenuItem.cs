// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public ICommand Command { get; }

        public string Header { get; }
        public List<MenuItem> Items { get; } = new();

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
