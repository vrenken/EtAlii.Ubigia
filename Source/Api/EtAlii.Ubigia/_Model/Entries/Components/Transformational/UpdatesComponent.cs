// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class UpdatesComponent : RelationsComponent
    {
        /// <inheritdoc />
        protected internal override string Name => "Updates";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.UpdatesComponent.Add(Relations, markAsStored);
        }
    }
}
