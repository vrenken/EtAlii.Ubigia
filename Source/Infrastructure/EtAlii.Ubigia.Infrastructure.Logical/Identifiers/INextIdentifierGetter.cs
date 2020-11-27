namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface INextIdentifierGetter
    {
        Identifier GetNext(Guid spaceId, in Identifier previousHeadIdentifier);
    }
}