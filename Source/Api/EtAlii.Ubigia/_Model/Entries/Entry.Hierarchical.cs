// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Parent => ((IComponentEditableEntry) this).ParentComponent.Relation;

        public Relation[] Children => _children.SelectMany(component => component.Relations).ToArray();
        private readonly ChildrenComponentCollection _children;

        ChildrenComponentCollection IEditableEntry.Children => _children;

        ChildrenComponentCollection IComponentEditableEntry.ChildrenComponent => _children;

        Relation IEditableEntry.Parent
        {
            get => ((IComponentEditableEntry) this).ParentComponent.Relation;
            set
            {
                if (((IComponentEditableEntry) this).ParentComponent.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Parent. This relation has already been assigned");
                }
                ((IComponentEditableEntry) this).ParentComponent = new ParentComponent { Relation = value };
            }
        }

        ParentComponent IComponentEditableEntry.ParentComponent { get; set; }
    }
}
