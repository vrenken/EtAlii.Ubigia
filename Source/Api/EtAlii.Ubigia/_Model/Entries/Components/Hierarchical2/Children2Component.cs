// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

public sealed class Children2Component : RelationsComponent
{
    /// <inheritdoc />
    protected internal override string Name => "Children2";

    /// <inheritdoc />
    protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
    {
        entry.AddChildren2(Relations, markAsStored);
    }
}
