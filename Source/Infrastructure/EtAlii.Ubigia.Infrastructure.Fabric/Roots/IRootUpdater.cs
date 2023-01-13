// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System;
using System.Threading.Tasks;

public interface IRootUpdater
{
    Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot);
}
