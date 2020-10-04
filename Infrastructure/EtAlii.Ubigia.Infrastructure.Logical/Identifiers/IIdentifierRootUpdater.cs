namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface IIdentifierRootUpdater
    {
        void Update(Guid spaceId, string name, Identifier id);

    }
}