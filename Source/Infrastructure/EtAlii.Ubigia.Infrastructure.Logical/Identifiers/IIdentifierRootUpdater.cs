namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IIdentifierRootUpdater
    {
        Task Update(Guid spaceId, string name, Identifier id);
    }
}