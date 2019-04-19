namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Logical;

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
        }
//
//        private ObservableCollection<Space> GetItems()
//        [
//            return _logicalContext.Spaces.GetItems()
//        }

        public async Task<Space> Add(Space item, SpaceTemplate template)
        {
            var addedSpace = _logicalContext.Spaces.Add(item, template, out bool isAdded);
            if (isAdded)
            {
                await _spaceInitializer.Initialize(addedSpace, template);
            }

            return addedSpace;
        }

        public Space Get(Guid accountId, string spaceName)
        {
            return _logicalContext.Spaces.Get(accountId, spaceName);
        }

        public Space Get(Guid itemId)
        {
            return _logicalContext.Spaces.Get(itemId);
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

        public void Remove(Space item)
        {
            _logicalContext.Spaces.Remove(item);
        }
    }
}