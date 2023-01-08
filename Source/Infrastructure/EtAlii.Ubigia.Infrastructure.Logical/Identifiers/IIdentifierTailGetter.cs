// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IIdentifierTailGetter
    {
        Task<Identifier> Get(Guid storageId, Guid spaceId);
    }
}
