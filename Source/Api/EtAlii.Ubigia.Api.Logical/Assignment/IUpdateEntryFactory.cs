// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

internal interface IUpdateEntryFactory
{
    Task<IEditableEntry> Create(
        IReadOnlyEntry entry,
        string tag,
        ExecutionScope scope);

    Task<IEditableEntry> Create(
        IReadOnlyEntry entry,
        ExecutionScope scope);
}
