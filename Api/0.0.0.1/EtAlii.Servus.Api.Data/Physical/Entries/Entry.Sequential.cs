namespace EtAlii.Servus.Api
{
    using System;

    public sealed partial class Entry
    {
        public Relation Previous { get { return _previous.Previous; } }
        private PreviousComponent _previous;

        public Relation Next { get { return _next.Next; } }
        private NextComponent _next;

        Relation IEditableEntry.Previous
        {
            get
            {
                return _previous.Previous;
            }
            set
            {
                if (_previous.Previous != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Previous. This relation has already been assigned");
                }
                _previous = new PreviousComponent(value);
            }
        }

        PreviousComponent IComponentEditableEntry.PreviousComponent
        {
            get
            {
                return _previous;
            }
            set
            {
                _previous = value;
            }
        }

        Relation IEditableEntry.Next
        {
            get
            {
                return _next.Next;
            }
            set
            {
                if (_next.Next != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Next. This property has already been assigned");
                }
                _next = new NextComponent(value);
            }
        }

        NextComponent IComponentEditableEntry.NextComponent
        {
            get
            {
                return _next;
            }
            set
            {
                _next = value;
            }
        }
    }
}
