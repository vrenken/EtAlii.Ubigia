namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public interface ILogicalSpaceSet
    {
        Space Get(Guid accountId, string spaceName);

        IEnumerable<Space> GetAll(Guid accountId);

        Space Add(Space item, SpaceTemplate template, out bool isAdded);

        IEnumerable<Space> GetAll();

        Space Get(Guid id);

        ObservableCollection<Space> GetItems();

        void Remove(Guid itemId);

        void Remove(Space itemToRemove);

        Space Update(Guid itemId, Space updatedItem);
    }
}