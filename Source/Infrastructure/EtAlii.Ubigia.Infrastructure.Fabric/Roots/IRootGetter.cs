namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRootGetter
    {
        IAsyncEnumerable<Root> GetAll(Guid spaceId);
        Task<Root> Get(Guid spaceId, Guid rootId);
        Task<Root> Get(Guid spaceId, string name);
    }
}