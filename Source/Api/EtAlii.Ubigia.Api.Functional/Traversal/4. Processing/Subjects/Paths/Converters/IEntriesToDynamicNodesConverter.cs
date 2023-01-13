// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

public interface IEntriesToDynamicNodesConverter
{
    IAsyncEnumerable<Node> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope);
    Task<Node> Convert(IReadOnlyEntry entry, ExecutionScope scope);
}
