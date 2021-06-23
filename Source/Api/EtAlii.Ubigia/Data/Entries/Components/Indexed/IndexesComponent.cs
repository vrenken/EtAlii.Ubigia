// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class IndexesComponent : RelationsComponent  
    {
        protected internal override string Name => _name;
        private const string _name = "Indexes";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.IndexesComponent.Add(Relations, markAsStored);
        }
    }
}
