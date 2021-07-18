// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class DowndateComponent : RelationComponent
    {
        /// <inheritdoc />
        protected internal override string Name => "Downdate";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.DowndateComponent = this;
        }
    }
}
