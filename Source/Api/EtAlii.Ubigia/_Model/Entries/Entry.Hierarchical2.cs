// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Parent2 => ((IComponentEditableEntry) this).Parent2Component.Relation;

        public Relation[] Children2 { get; private set; }

        private readonly Children2ComponentCollection _children2Component;

        IReadOnlyRelationsComponentCollection<Children2Component> IEditableEntry.Children2 => _children2Component;

        IReadOnlyRelationsComponentCollection<Children2Component> IComponentEditableEntry.Children2Component => _children2Component;

        Relation IEditableEntry.Parent2
        {
            get => ((IComponentEditableEntry) this).Parent2Component.Relation;
            set
            {
                if (((IComponentEditableEntry) this).Parent2Component.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Parent2. This relation has already been assigned");
                }
                ((IComponentEditableEntry) this).Parent2Component = new Parent2Component { Relation = value };
            }
        }

        Parent2Component IComponentEditableEntry.Parent2Component { get; set; }

        void IEditableEntry.AddChild2(in Identifier id)
        {
            _children2Component.Add(id);
            Children2 = _children2Component
                .SelectMany(components => components.Relations)
                .ToArray();
        }

        void IComponentEditableEntry.AddChildren2(Relation[] relations, bool markAsStored)
        {
            _children2Component.Add(relations, markAsStored);
            Children2 = _children2Component
                .SelectMany(components => components.Relations)
                .ToArray();
        }
    }
}
