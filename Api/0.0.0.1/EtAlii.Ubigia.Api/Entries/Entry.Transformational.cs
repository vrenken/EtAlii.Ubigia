namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Downdate => _downdate.Relation;
        private DowndateComponent _downdate;

        public IEnumerable<Relation> Updates { get { return _updates.SelectMany(components => components.Relations); } }
        private readonly UpdatesComponentCollection _updates;

        Relation IEditableEntry.Downdate
        {
            get
            {
                return _downdate.Relation;
            }
            set
            {
                if (_downdate.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Downdate. This relation has already been assigned");
                }
                _downdate = new DowndateComponent { Relation = value };
            }
        }

        DowndateComponent IComponentEditableEntry.DowndateComponent
        {
            get { return _downdate; }
            set { _downdate = value; }
        }

        UpdatesComponentCollection IEditableEntry.Updates => _updates;

        UpdatesComponentCollection IComponentEditableEntry.UpdatesComponent => _updates;
    }
}
