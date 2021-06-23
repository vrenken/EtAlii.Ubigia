// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Parent2 => ((IComponentEditableEntry) this).Parent2Component.Relation;

        public IEnumerable<Relation> Children2 => _children2.SelectMany(component => component.Relations);
        private readonly Children2ComponentCollection _children2;


        Children2ComponentCollection IEditableEntry.Children2 => _children2;

        Children2ComponentCollection IComponentEditableEntry.Children2Component => _children2;

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
    }
}
