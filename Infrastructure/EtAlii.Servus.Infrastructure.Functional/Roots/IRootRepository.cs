namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

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