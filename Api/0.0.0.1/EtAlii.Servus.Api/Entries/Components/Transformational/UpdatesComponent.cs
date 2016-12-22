﻿namespace EtAlii.Servus.Api
{
    public class UpdatesComponent : RelationsComponent 
    {
        protected internal override string Name { get { return _name; } }
        private const string _name = "Updates";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.UpdatesComponent.Add(Relations, markAsStored);
        }
    }
}
