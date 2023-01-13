// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

public interface IGraphUnlinker
{
    Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope);
}
