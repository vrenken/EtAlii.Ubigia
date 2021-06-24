// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ItemRemover : IItemRemover
    {
        public void Remove<T>(IList<T> items, Guid itemId)
            where T : class, IIdentifiable
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentException("No item ID specified");
            }

            var itemToRemove = items.SingleOrDefault(item => item.Id == itemId);
            Remove(items, itemToRemove);
        }

        public void Remove<T>(IList<T> items, T itemToRemove)
            where T : class, IIdentifiable
        {
            if (itemToRemove == null)
            {
                throw new ArgumentNullException(nameof(itemToRemove),"No item specified");
            }

            itemToRemove = items.SingleOrDefault(item => item.Id == itemToRemove.Id);
            if (itemToRemove != null)
            {
                try
                {
                    items.Remove(itemToRemove);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to remove item", e);
                }
            }
            else
            {
                throw new InvalidOperationException("No item found to remove");
            }
        }

    }
}