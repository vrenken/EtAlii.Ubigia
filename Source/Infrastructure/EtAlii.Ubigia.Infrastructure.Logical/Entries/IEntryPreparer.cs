﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System;
using System.Threading.Tasks;

public interface IEntryPreparer
{
    Task<Entry> Prepare(Guid storageId, Guid spaceId);
    Task<Entry> Prepare(Identifier id);
}
