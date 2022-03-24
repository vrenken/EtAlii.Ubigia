// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class ParentComponent : RelationComponent
    {
        /// <inheritdoc />
        protected internal override string Name => "Parent";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ParentComponent = this;
        }
    }
}
