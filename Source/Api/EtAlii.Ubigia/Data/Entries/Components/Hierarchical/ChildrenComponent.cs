// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class ChildrenComponent : RelationsComponent
    {
        protected internal override string Name => "Children";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ChildrenComponent.Add(Relations, markAsStored);
        }
    }
}
