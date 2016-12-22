namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface IIdentifierTailGetter
    {
        Identifier Get(Guid spaceId);
    }
}