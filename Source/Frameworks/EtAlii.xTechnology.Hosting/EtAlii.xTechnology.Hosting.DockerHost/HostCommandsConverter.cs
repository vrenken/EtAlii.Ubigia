// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using System.Linq;

    internal class HostCommandsConverter : IHostCommandsConverter
    {
        public MenuItem[] ToMenuItems(ICommand[] commands)
        {
            var result = new List<MenuItem>();

            foreach (var command in commands)
            {
                var nameParts = command.Name.Split('/');

                MenuItem parent = null;

                for (var i = 0; i < nameParts.Length; i++)
                {
                    var collectionToAddTo = (IList<MenuItem>)parent?.Items ?? result;
                    if (i == nameParts.Length - 1)
                    {
                        var menuItem = new MenuItem(nameParts[i], command);
                        collectionToAddTo.Add(menuItem);
                    }
                    else
                    {
                        var newHeader = nameParts[i];
                        var menuItem = collectionToAddTo.FirstOrDefault(item => item.Header == newHeader);
                        if (menuItem == null)
                        {
                            menuItem = new MenuItem(newHeader);
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