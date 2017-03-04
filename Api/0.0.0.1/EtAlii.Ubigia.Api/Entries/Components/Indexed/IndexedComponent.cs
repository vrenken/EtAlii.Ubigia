﻿namespace EtAlii.Ubigia.Api
{
    public class IndexedComponent : RelationComponent
    {
        protected internal override string Name => _name;
        private const string _name = "Indexed";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexedComponent = this;
        }
    }
}
