// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public class ParentMenuItemFinder
    {
        public MenuItem Find(MenuItem menuItemToFindParentFor, MenuItem[] rootMenuItems, MenuItem parentMenuItem = null)
        {
            foreach (var rootMenuItem in rootMenuItems)
            {
                if (rootMenuItem == menuItemToFindParentFor)
                {
                    return parentMenuItem;
                }
                var matchingParent = Find(menuItemToFindParentFor, rootMenuItem.Items.ToArray(), rootMenuItem);
                if (matchingParent != null)
                {
                    return matchingParent;
                }
            }
            return null;
        }
    }
}