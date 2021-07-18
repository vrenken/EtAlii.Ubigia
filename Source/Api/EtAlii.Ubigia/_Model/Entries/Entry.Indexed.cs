// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public IEnumerable<Relation> Indexes => _indexes.SelectMany(component => component.Relations);
        private readonly IndexesComponentCollection _indexes;

        public Relation Indexed => ((IComponentEditableEntry) this).IndexedComponent.Relation;

        IndexesComponentCollection IEditableEntry.Indexes => _indexes;

        IndexesComponentCollection IComponentEditableEntry.IndexesComponent => _indexes;

        Relation IEditableEntry.Indexed
        {
            get => ((IComponentEditableEntry) this).IndexedComponent.Relation;
            set
            {
                if (((IComponentEditableEntry) this).IndexedComponent.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Indexed. This relation has already been assigned");
                }
                ((IComponentEditableEntry) this).IndexedComponent = new IndexedComponent { Relation = value };
            }
        }

        IndexedComponent IComponentEditableEntry.IndexedComponent { get; set; }
    }
}
