namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;

    public interface IRootRepository
    {
        IEnumerable<Root> GetAll(Guid spaceId);

        Root Get(Guid spaceId, Guid rootId);
        Root Get(Guid spaceId, string name);

        Root Add(Guid spaceId, Root root);

        void Remove(Guid spaceId, Guid rootId);
        void Remove(Guid spaceId, Root root);

        Root Update(Guid spaceId, Guid rootId, Root root);
    }
}