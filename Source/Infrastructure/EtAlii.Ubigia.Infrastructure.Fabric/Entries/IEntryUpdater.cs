// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEntryUpdater
{
    Task Update(Entry entry, IEnumerable<IComponent> changedComponents);
    Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);
}
