namespace EtAlii.Ubigia.Api
{
    using System;

    public sealed partial class Entry
    {
        public Relation Previous => _previous.Relation;
        private PreviousComponent _previous;

        public Relation Next => _next.Relation;
        private NextComponent _next;

        Relation IEditableEntry.Previous
        {
            get { return _previous.Relation; }
            set
            {
                if (_previous.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Previous. This relation has already been assigned");
                }
                _previous = new PreviousComponent { Relation = value };
            }
        }

        PreviousComponent IComponentEditableEntry.PreviousComponent
        {
            get { return _previous; }
            set { _previous = value; }
        }

        Relation IEditableEntry.Next
        {
            get { return _next.Relation; }
            set
            {
                if (_next.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Next. This property has already been assigned");
                }
                _next = new NextComponent { Relation = value };
            }
        }

        NextComponent IComponentEditableEntry.NextComponent
        {
            get { return _next; }
            set { _next = value; }
        }
    }
}
