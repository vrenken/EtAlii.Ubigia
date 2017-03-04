﻿namespace EtAlii.Ubigia.Api
{
    public class ChildrenComponent : RelationsComponent 
    {
        protected internal override string Name => _name;
        private const string _name = "Children";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ChildrenComponent.Add(Relations, markAsStored);
        }
    }
}
