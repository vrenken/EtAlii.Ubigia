// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class IdentifierComponent : NonCompositeComponent
    {
        internal IdentifierComponent()
        {
        }

        public Identifier Id { get; internal set; }

        /// <inheritdoc />
        protected internal override string Name => "Identifier";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IdComponent = this;
        }
    }
}
