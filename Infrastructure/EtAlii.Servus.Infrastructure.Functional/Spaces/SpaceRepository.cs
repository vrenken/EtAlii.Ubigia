namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Logical;

    internal class SpaceRepository : ISpaceRepository
    {
        private readonly ILogicalContext _logicalContext;
        private readonly ISpaceInitializer _spaceInitializer;

        public SpaceRepository(
            ILogicalContext logicalContext, 
            ISpaceInitializer spaceInitializer)
        {
            _spaceInitializer = spaceInitializer;
            _logicalContext = logicalContext;
            _logicalContext.Spaces.Added += OnSpaceAdded;
        }

        private void OnSpaceAdded(object sender, SpaceAddedEventArgs e)
        {
            _spaceInitializer.Initialize(e.Space, e.Template);
        }

        private ObservableCollection<Space> GetItems()
        {
            return _logicalContext.Spaces.GetItems();
        }

        public Space Add(Space item, SpaceTemplate template)
        {
            return _logicalContext.Spaces.Add(item, template);
        }

        public Space Get(Guid accountId, string spaceName)
        {
            return _logicalContext.Spaces.Get(accountId, spaceName);
        }

        public Space Get(Guid id)
        {
            return _logicalContext.Spaces.Get(id);
        }

        public IEnumerable<Space> GetAll()
        {
            return _logicalContext.Spaces.GetAll();
        }


        public IEnumerable<Space> GetAll(Guid accountId)
        {
            return _logicalContext.Spaces.GetAll(accountId);
        }

        public Space Update(Guid itemId, Space updatedItem)
        {
            return _logicalContext.Spaces.Update(itemId, updatedItem);
        }



        public void Remove(Guid itemId)
        {
            _logicalContext.Spaces.Remove(itemId);
        }

        public void Remove(Space itemToRemove)
        {
            _logicalContext.Spaces.Remove(itemToRemove);
        }
    }
}