// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;

    public class ConsoleDialog
    {
        private readonly IHost _host;
        private MenuItem _currentMenuItem;
        private MenuItem[] _selectableChildMenuItems;
        private MenuItem[] _rootMenuItems;
        private readonly ParentMenuItemFinder _parentMenuItemFinder;
        private readonly HostCommandsConverter _hostCommandsConverter;

        public ConsoleDialog(IHost host)
        {
            _parentMenuItemFinder = new ParentMenuItemFinder();
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;

            _hostCommandsConverter = new HostCommandsConverter();

            _rootMenuItems = _hostCommandsConverter.ToMenuItems(_host.Commands);
        }

        private void OnHostPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_host.State):
                    OnHostStateChanged(_host.State);
                    break;
                case nameof(_host.Commands):
                    _rootMenuItems = _hostCommandsConverter.ToMenuItems(_host.Commands);
                    _currentMenuItem = null;
                    WriteHeaderAndStatus();
                    UpdateSelectableMenuItems();
                    WriteOptions();
                    break;
            }
        }

        private void OnHostStateChanged(State state)
        {
            switch (state)
            {
                case State.Shutdown:
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Host has shut down. Have a nice day.");
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine();
        }

        public void Start()
        {
            while (true)
            {
                WriteHeaderAndStatus();
                UpdateSelectableMenuItems();
                WriteOptions();
                _currentMenuItem = HandleInput();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void UpdateSelectableMenuItems()
        {
            _selectableChildMenuItems = _currentMenuItem?.Items?.ToArray() ?? _rootMenuItems;
        }

        private MenuItem HandleInput()
        {
            var consoleKey = Console.In.ReadLine();

            if (consoleKey == "0")
            {
                _currentMenuItem = _parentMenuItemFinder.Find(_currentMenuItem, _rootMenuItems);
            }
            else if (int.TryParse(consoleKey, out var inputNumber))
            {
                var menuItemIndex = inputNumber - 1;
                if (menuItemIndex < 0 || menuItemIndex >= _selectableChildMenuItems.Length) return _currentMenuItem;

                var menuItem = _selectableChildMenuItems[menuItemIndex];
                if (menuItem.Command != null)
                {
                    if (menuItem.Command.CanExecute)
                    {
                        menuItem.Command.Execute();
                    }
                }
                else
                {
                    _currentMenuItem = menuItem;
                }
            }

            return _currentMenuItem;
        }

        private void WriteOptions()
        {
            var path = _currentMenuItem != null ? ToPath(_currentMenuItem) : "";

            Console.WriteLine("-----------------------------");
            Console.WriteLine("[Options]");
            if (!string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine(path);
            }

            if (_currentMenuItem != null)
            {
                Console.WriteLine("0: Back");
            }

            for (var i = 0; i < _selectableChildMenuItems.Length; i++)
            {
                var menuItem = _selectableChildMenuItems[i];
                var availability = menuItem.Command?.CanExecute == false ? "(UNAVAILABLE)" : string.Empty;
                Console.WriteLine($"{i + 1}: {menuItem.Header} {availability}");
            }
        }

        private void WriteHeaderAndStatus()
        {
            Console.Clear();
            foreach (var status in _host.Status)
            {
                if (string.IsNullOrWhiteSpace(status.Summary)) continue;

                Console.WriteLine($"[{status.Id}]");
                Console.WriteLine(status.Summary.TrimEnd(Environment.NewLine.ToCharArray()));
                Console.WriteLine();
            }
        }

        private string ToPath(MenuItem menuItem)
        {
            var result = menuItem.Header;
            while (menuItem != null)
            {
                menuItem = _parentMenuItemFinder.Find(menuItem, _rootMenuItems);
                result = (menuItem != null ? menuItem.Header + " / " : "") + result;
            }
            return result;
        }
    }
}
