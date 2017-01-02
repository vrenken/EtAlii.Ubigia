namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IIdentifierTailGetter
    {
        Identifier Get(Guid spaceId);
    }
}