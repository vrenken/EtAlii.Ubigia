namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IIdentifierRootUpdater
    {
        void Update(Guid spaceId, string name, Identifier id);

    }
}