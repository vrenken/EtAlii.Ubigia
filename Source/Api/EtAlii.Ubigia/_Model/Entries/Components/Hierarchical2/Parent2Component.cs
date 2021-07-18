// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class Parent2Component : RelationComponent
    {
        /// <inheritdoc />
        protected internal override string Name => "Parent2";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Parent2Component = this;
        }
    }
}
