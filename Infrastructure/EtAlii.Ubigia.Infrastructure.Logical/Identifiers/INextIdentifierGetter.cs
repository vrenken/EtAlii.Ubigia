namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface INextIdentifierGetter
    {
        Identifier GetNext(Guid spaceId, Identifier previousHeadIdentifier);
    }
}