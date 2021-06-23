// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class IdentifierComponent : NonCompositeComponent
    {
        internal IdentifierComponent()
        {
        }

        public Identifier Id { get; internal set; }

        protected internal override string Name => "Identifier";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IdComponent = this;
        }
    }
}
