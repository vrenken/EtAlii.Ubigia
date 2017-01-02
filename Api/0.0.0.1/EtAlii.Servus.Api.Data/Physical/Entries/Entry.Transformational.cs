namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Downdate { get { return _downdate.Downdate; } }
        private DowndateComponent _downdate;

        public IEnumerable<Relation> Updates { get { return _updates.SelectMany(components => components.Updates); } }
        private readonly UpdatesComponentCollection _updates;

        Relation IEditableEntry.Downdate
        {
            get
            {
                return _downdate.Downdate;
            }
            set
            {
                if (_downdate.Downdate != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Downdate. This relation has already been assigned");
                }
                _downdate = new DowndateComponent(value);
            }
        }

        DowndateComponent IComponentEditableEntry.DowndateComponent
        {
            get
            {
                return _downdate;
            }
            set
            {
                _downdate = value;
            }
        }

        UpdatesComponentCollection IEditableEntry.Updates
        {
            get
            {
                return _updates;
            }
        }

        UpdatesComponentCollection IComponentEditableEntry.UpdatesComponent
        {
            get
            {
                return _updates;
            }
        }
    }
}
