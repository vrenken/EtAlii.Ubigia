// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

public interface IGraphAdder
{
    Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope);
    Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope);
}
