namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Threading.Tasks;

    public interface IRootAdder
    {
        Task<Root> Add(Guid spaceId, Root root);
    }
}