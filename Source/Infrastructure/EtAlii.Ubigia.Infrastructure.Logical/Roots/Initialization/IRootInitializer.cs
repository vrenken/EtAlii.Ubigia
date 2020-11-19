namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IRootInitializer
    {
        Task Initialize(Guid spaceId, Root root);
    }
}