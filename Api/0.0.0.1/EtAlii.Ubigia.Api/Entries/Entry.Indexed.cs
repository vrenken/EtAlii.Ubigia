namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public IEnumerable<Relation> Indexes { get { return _indexes.SelectMany(component => component.Relations); } }
        private readonly IndexesComponentCollection _indexes;

        public Relation Indexed => _indexed.Relation;
        private IndexedComponent _indexed;


        IndexesComponentCollection IEditableEntry.Indexes => _indexes;

        IndexesComponentCollection IComponentEditableEntry.IndexesComponent => _indexes;

        Relation IEditableEntry.Indexed
        {
            get { return _indexed.Relation; }
            set
            {
                if (_indexed.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Indexed. This relation has already been assigned");
                }
                _indexed = new IndexedComponent { Relation = value };
            }
        }

        IndexedComponent IComponentEditableEntry.IndexedComponent
        {
            get { return _indexed; }
            set { _indexed = value; }
        }
    }
}
