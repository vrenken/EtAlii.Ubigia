// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalSpaceSet : ILogicalSpaceSet
    {
        private readonly IFabricContext _fabric;
        private readonly object _lockObject = new();

        private const string Folder = "Spaces";

        private ObservableCollection<Space> Items { get { lock (_lockObject) { return _items ??= InitializeItems(); } } }
        private ObservableCollection<Space> _items; // We don't us a Lazy construction here because the first get of this property is actually cascaded through the logical layer. A Lazy instance results in a deadlock.

        public LogicalSpaceSet(
            IFabricContext fabric)
        {
            _fabric = fabric;
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Space> GetAll(Guid accountId)
        {
            return Items
                .Where(space => space.AccountId == accountId)
                .ToAsyncEnumerable();
        }

        /// <inheritdoc />
        public IAsyncEnumerable<Space> GetAll()
        {
            return _fabric.Items.GetAll(Items);
        }

        /// <inheritdoc />
        public Task<Space> Get(Guid id)
        {
            return _fabric.Items.Get(Items, id);
        }

        /// <inheritdoc />
        public Task<Space> Get(Guid accountId, string spaceName)
        {
            var space = Items.SingleOrDefault(space => space.AccountId == accountId && space.Name == spaceName);
            return Task.FromResult(space);
        }


        private Space UpdateFunction(Space originalItem, Space updatedItem)
        {
            originalItem.Name = updatedItem.Name;
            return originalItem;
        }

        private bool CanAddFunction(IList<Space> items, Space item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "No item specified");
            }

            var canAdd = !string.IsNullOrWhiteSpace(item.Name);
            if (canAdd)
            {
                canAdd = item.Id == Guid.Empty;
            }
            if (canAdd)
            {
                canAdd = !items.Any(i => (string.CompareOrdinal(i.Name, item.Name) == 0 && i.AccountId == item.AccountId) || i.Id == item.Id);
            }
            return canAdd;
        }


        private ObservableCollection<Space> InitializeItems()
        {
            var task = _fabric.Items.GetItems<Space>(Folder);
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public async Task<(Space, bool)> Add(Space item, SpaceTemplate template)
        {
            var space = await _fabric.Items
                .Add(Items, CanAddFunction, item)
                .ConfigureAwait(false);
            var isAdded = space != null;
            return (space, isAdded);
        }

        /// <inheritdoc />
        public Task Remove(Guid itemId)
        {
            return _fabric.Items.Remove(Items, itemId);
        }

        /// <inheritdoc />
        public Task Remove(Space itemToRemove)
        {
            return _fabric.Items.Remove(Items, itemToRemove);
        }

        /// <inheritdoc />
        public Task<Space> Update(Guid itemId, Space updatedItem)
        {
            return _fabric.Items.Update(Items, UpdateFunction, Folder, itemId, updatedItem);
        }
    }
}
