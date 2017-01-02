namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface ILogicalRootSet
    {
        Root Add(Guid spaceId, Root root);

        IEnumerable<Root> GetAll(Guid spaceId);
        Root Get(Guid spaceId, Guid rootId);
        Root Get(Guid spaceId, string name);


        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);

        Root Update(Guid spaceId, Guid rootId, Root updatedRoot);

        void Start();
        void Stop();

        event EventHandler<RootAddedEventArgs> Added;
    }
}