namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;

    public class ConsoleDialog
    {
        private readonly IHost _host;
        private MenuItem _currentMenuItem;
        private readonly MenuItem[] _rootMenuItems;
        private readonly ParentMenuItemFinder _parentMenuItemFinder;

        public ConsoleDialog(IHost host)
        {
            _parentMenuItemFinder = new ParentMenuItemFinder();
            _host = host;
            _host.StatusChanged += OnHostOnStatusChanged;

            var hostCommandsConverter = new HostCommandsConverter();

            _rootMenuItems = hostCommandsConverter.ToMenuItems(_host.Commands);
        }

        private void OnHostOnStatusChanged(HostStatus status)
        {
            switch (status)
            {
                case HostStatus.Shutdown:
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Host status: {_host.Status}");
                Console.WriteLine();

                var currentMenuItems = _currentMenuItem?.Items?.ToArray() ?? _rootMenuItems;

                var path = _currentMenuItem != null ? ToPath(_currentMenuItem) : "";
                Console.WriteLine($"{path}");
                Console.WriteLine();

                for (int i = 0; i < currentMenuItems.Length; i++)
                {
                    var menuItem = currentMenuItems[i];
                    var availability = menuItem.Command?.CanExecute == false ? "(UNAVAILABLE)" : String.Empty;
                    Console.WriteLine($"{i + 1}: {menuItem.Header} {availability}");
                }

                var consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == ConsoleKey.Backspace || consoleKeyInfo.Key == ConsoleKey.Escape)
                {
                    _currentMenuItem = _parentMenuItemFinder.Find(_currentMenuItem, _rootMenuItems);
                }
                else if (char.IsDigit(consoleKeyInfo.KeyChar))
                {
                    var menuItemIndex = Int32.Parse($"{consoleKeyInfo.KeyChar}") - 1;
                    if (menuItemIndex >= 0 && menuItemIndex < currentMenuItems.Length)
                    {
                        var menuItem = currentMenuItems[menuItemIndex];
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
                }
            }
        }

        private string ToPath(MenuItem menuItem)
        {
            var result = menuItem.Header;
            while (menuItem != null)
            {
                menuItem = _parentMenuItemFinder.Find(menuItem, _rootMenuItems);
                result = (menuItem != null ? menuItem?.Header + " / " : "") + result;
            }
            return result;
        }
    }
}