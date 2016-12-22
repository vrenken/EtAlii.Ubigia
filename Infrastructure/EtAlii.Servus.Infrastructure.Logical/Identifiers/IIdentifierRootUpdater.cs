namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IIdentifierRootUpdater
    {
        void Update(Guid spaceId, string name, Identifier id);

    }
}