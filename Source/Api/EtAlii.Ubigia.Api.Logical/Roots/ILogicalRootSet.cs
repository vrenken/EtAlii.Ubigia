// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILogicalRootSet
{
    Task<Root> Add(string name, RootType rootType);
    Task Remove(Guid id);
    Task<Root> Change(Guid rootId, string rootName, RootType rootType);
    Task<Root> Get(string rootName);
    Task<Root> Get(Guid rootId);
    IAsyncEnumerable<Root> GetAll();
}
