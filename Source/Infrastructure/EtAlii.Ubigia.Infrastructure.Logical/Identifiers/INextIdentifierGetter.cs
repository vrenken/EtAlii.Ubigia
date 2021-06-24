// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;

    public interface INextIdentifierGetter
    {
        Identifier GetNext(Guid spaceId, in Identifier previousHeadIdentifier);
    }
}