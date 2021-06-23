// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class TagComponent : NonCompositeComponent
    {
        internal TagComponent()
        {
        }

        public string Tag { get; internal set; }

        protected internal override string Name => _name;
        private const string _name = "Tag";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TagComponent = this;
        }
    }
}
