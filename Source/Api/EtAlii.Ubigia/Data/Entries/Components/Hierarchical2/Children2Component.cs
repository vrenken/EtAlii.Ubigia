﻿namespace EtAlii.Ubigia
{
    public class Children2Component : RelationsComponent  
    {
        protected internal override string Name => _name;
        private const string _name = "Children2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Children2Component.Add(Relations, markAsStored);
        }
    }
}
