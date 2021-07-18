// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class NextComponent : RelationComponent
    {
        protected internal override string Name => "Next";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.NextComponent = this;
        }
    }
}
