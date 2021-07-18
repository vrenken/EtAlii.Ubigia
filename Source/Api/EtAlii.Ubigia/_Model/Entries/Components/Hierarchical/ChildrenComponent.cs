// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class ChildrenComponent : RelationsComponent
    {
        /// <inheritdoc />
        protected internal override string Name => "Children";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ChildrenComponent.Add(Relations, markAsStored);
        }
    }
}
