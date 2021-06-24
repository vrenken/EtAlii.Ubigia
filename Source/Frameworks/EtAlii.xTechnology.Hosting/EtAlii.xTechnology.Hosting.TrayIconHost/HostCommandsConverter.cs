// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using System.Linq;

    internal class HostCommandsConverter : IHostCommandsConverter
    {
        public MenuItemViewModel[] ToViewModels(ICommand[] commands)
        {
            var result = new List<MenuItemViewModel>();

            foreach (var command in commands)
            {
                var nameParts = command.Name.Split('/');

                MenuItemViewModel parent = null;

                for (var i = 0; i < nameParts.Length; i++)
                {
                    var collectionToAddTo = (IList<MenuItemViewModel>)parent?.Items ?? result;
                    if (i == nameParts.Length - 1)
                    {
                        var menuItem = new MenuItemViewModel(nameParts[i], new WrappedHostCommand(command));
                        collectionToAddTo.Add(menuItem);
                    }
                    else
                    {
                        var newHeader = nameParts[i];
                        var menuItem = collectionToAddTo.FirstOrDefault(item => item.Header == newHeader);
                        if (menuItem == null)
                        {
                            menuItem = new MenuItemViewModel(newHeader);
                            collectionToAddTo.Add(menuItem);
                        }
                        parent = menuItem;
                    }
                }
            }
            return result.ToArray();
        }
    }
}