// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class TypeComponent : NonCompositeComponent
    {
        internal TypeComponent()
        {
        }

        public string Type { get; internal set; }

        protected internal override string Name => "Type";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TypeComponent = this;
        }
    }
}
