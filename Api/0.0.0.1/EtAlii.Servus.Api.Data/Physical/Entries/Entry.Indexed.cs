namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public IEnumerable<Relation> Indexes { get { return _indexes.SelectMany(component => component.Indexes); } }
        private readonly IndexesComponentCollection _indexes;

        public Relation Indexed { get { return _indexed.Indexed; } }
        private IndexedComponent _indexed;


        IndexesComponentCollection IEditableEntry.Indexes
        {
            get
            {
                return _indexes;
            }
        }

        IndexesComponentCollection IComponentEditableEntry.IndexesComponent
        {
            get
            {
                return _indexes;
            }
        }

        Relation IEditableEntry.Indexed
        {
            get
            {
                return _indexed.Indexed;
            }
            set
            {
                if (_indexed.Indexed != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Indexed. This relation has already been assigned");
                }
                _indexed = new IndexedComponent(value);
            }
        }

        IndexedComponent IComponentEditableEntry.IndexedComponent
        {
            get
            {
                return _indexed;
            }
            set
            {
                _indexed = value;
            }
        }
    }
}
