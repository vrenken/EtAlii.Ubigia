// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation[] Indexes => _indexes;
        private Relation[] _indexes;
        private readonly IndexesComponentCollection _indexesComponent;

        public Relation Indexed => ((IComponentEditableEntry) this).IndexedComponent.Relation;

        IReadOnlyRelationsComponentCollection<IndexesComponent> IEditableEntry.Indexes => _indexesComponent;

        IReadOnlyRelationsComponentCollection<IndexesComponent> IComponentEditableEntry.IndexesComponent => _indexesComponent;

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

        void IEditableEntry.AddIndex(in Identifier id)
        {
            _indexesComponent.Add(id);
            _indexes = _indexesComponent
                .SelectMany(components => components.Relations)
                .ToArray();
        }

        void IComponentEditableEntry.AddIndexes(Relation[] relations, bool markAsStored)
        {
            _indexesComponent.Add(relations, markAsStored);
            _indexes = _indexesComponent
                .SelectMany(components => components.Relations)
                .ToArray();
        }
    }
}
