namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalSpaceSet : ILogicalSpaceSet
    {
        private readonly IFabricContext _fabric;
        private readonly object _lockObject = new object();

        private const string _folder = "Spaces";

        private ObservableCollection<Space> Items { get { lock (_lockObject) { return _items ?? (_items = InitializeItems()); } } }
        private ObservableCollection<Space> _items; // We don't us a Lazy construction here because the first get of this property is actually cascaded through the logical layer. A Lazy instance results in a deadlock.

        public event EventHandler<SpaceAddedEventArgs> Added { add { _added += value; } remove { var added = _added; if (added != null) added -= value; } }
        private EventHandler<SpaceAddedEventArgs> _added;

        public LogicalSpaceSet(
            IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public Space Get(Guid accountId, string spaceName)
        {
            return Items.SingleOrDefault(space => space.AccountId == accountId && space.Name == spaceName);
        }

        public IEnumerable<Space> GetAll(Guid accountId)
        {
            return Items.Where(space => space.AccountId == accountId);
        }

        private Space UpdateFunction(Space originalItem, Space updatedItem)
        {
            originalItem.Name = updatedItem.Name;
            return originalItem;
        }

        private bool CannAddFunction(IList<Space> items, Space item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("No item specified");
            }

            var canAdd = !String.IsNullOrWhiteSpace(item.Name);
            if (canAdd)
            {
                canAdd = item.Id == Guid.Empty;
            }
            if (canAdd)
            {
                canAdd = !items.Any(i => (String.CompareOrdinal(i.Name, item.Name) == 0 && i.AccountId == item.AccountId) || i.Id == item.Id);
            }
            return canAdd;
        }


        private ObservableCollection<Space> InitializeItems()
        {
            var items = _fabric.Items.GetItems<Space>(_folder);
            return items;
        }

        public Space Add(Space item, SpaceTemplate template)
        {
            var space = _fabric.Items.Add(Items, CannAddFunction, item);
            if (space != null)
            {
                var e =  new SpaceAddedEventArgs(space, template);
                _added?.Invoke(this, e);
            }
            return space;
        }

        public IEnumerable<Space> GetAll()
        {
            return _fabric.Items.GetAll(Items);
        }

        public Space Get(Guid id)
        {
            return _fabric.Items.Get(Items, id);
        }

        public ObservableCollection<Space> GetItems()
        {
            return _fabric.Items.GetItems<Space>(_folder);
        }

        public void Remove(Guid itemId)
        {
            _fabric.Items.Remove(Items, itemId);
        }

        public void Remove(Space itemToRemove)
        {
            _fabric.Items.Remove(Items, itemToRemove);
        }

        public Space Update(Guid itemId, Space updatedItem)
        {
            return _fabric.Items.Update(Items, UpdateFunction, _folder, itemId, updatedItem);
        }
    }
}