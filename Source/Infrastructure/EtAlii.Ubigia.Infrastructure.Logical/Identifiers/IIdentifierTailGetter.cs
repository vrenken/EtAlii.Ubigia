namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface IIdentifierTailGetter
    {
        Identifier Get(Guid spaceId);
    }
}