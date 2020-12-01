﻿namespace EtAlii.Ubigia
{
    public class IdentifierComponent : NonCompositeComponent
    {
        internal IdentifierComponent()
        { 
        }

        public Identifier Id { get; internal set; }

        protected internal override string Name => _name;
        private const string _name = "Identifier";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IdComponent = this;
        }
    }
}
