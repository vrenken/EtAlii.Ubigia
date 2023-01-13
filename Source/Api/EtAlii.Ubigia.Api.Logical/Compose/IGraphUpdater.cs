// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

public interface IGraphUpdater
{
    Task<IEditableEntry> Update(IReadOnlyEntry entry, ExecutionScope scope);
    Task<IEditableEntry> Update(IReadOnlyEntry entry, string newType, ExecutionScope scope);
}
