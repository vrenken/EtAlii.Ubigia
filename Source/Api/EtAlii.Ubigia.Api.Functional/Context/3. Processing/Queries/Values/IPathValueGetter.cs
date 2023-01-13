// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Traversal;

internal interface IPathValueGetter
{
    Task<Value> Get(string valueName, Structure structure, PathSubject path, ExecutionScope scope);
}
