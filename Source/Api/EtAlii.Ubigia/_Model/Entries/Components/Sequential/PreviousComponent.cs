// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class PreviousComponent : RelationComponent
    {
        protected internal override string Name => "Previous";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.PreviousComponent = this;
        }
    }
}
