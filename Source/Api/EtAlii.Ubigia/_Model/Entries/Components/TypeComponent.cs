// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class TypeComponent : NonCompositeComponent
    {
        internal TypeComponent()
        {
        }

        public string Type { get; internal set; }

        /// <inheritdoc />
        protected internal override string Name => "Type";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TypeComponent = this;
        }
    }
}
