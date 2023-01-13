// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

public interface IGraphComposer
{
    Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope);
    Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope);
    Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope);
    Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope);

    Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope);
    Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope);

    Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope);
}
