namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Api;

    public interface INextIdentifierGetter
    {
        Identifier GetNext(Guid spaceId, Identifier previousHeadIdentifier);
    }
}