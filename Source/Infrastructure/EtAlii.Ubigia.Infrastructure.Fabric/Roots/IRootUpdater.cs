namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Threading.Tasks;

    public interface IRootUpdater
    {
        Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot);
    }
}