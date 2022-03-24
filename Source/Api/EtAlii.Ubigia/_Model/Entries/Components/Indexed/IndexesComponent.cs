// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class IndexesComponent : RelationsComponent
    {
        protected internal override string Name => "Indexes";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexesComponent.Add(Relations, markAsStored);
        }
    }
}
