namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;

    [JsonObject(MemberSerialization.Fields)]
    public sealed partial class Entry : IEquatable<Entry>, IReadOnlyEntry, IEditableEntry, IComponentEditableEntry
    {
        [JsonConstructor]
        private Entry()
        {
            // Temporal
            _downdate = new DowndateComponent();
            _updates = new UpdatesComponentCollection();

            // Hierarchical
            _parent = new ParentComponent();
            _children = new ChildrenComponentCollection();

            // Hierarchical
            _parent2 = new Parent2Component();
            _children2 = new Children2ComponentCollection();

            // Indexed
            _indexes = new IndexesComponentCollection();
            _indexed = new IndexedComponent();

            // Sequential
            _previous = new PreviousComponent();
            _next = new NextComponent();

            _type = new TypeComponent();
            _id = new IdentifierComponent();
        }

        private Entry(Identifier id)
            : this()
        {
            _id = new IdentifierComponent(id);
        }

        private Entry(Identifier id, Relation previous)
            : this(id)
        {
            _previous = new PreviousComponent(previous);
        }

        public Entry(Identifier id, Relation previous, object content)
            : this(id)
        {
            _previous = new PreviousComponent(previous);
        }

        public Identifier Id { get { return _id.Id; } }
        private IdentifierComponent _id;

        Identifier IEditableEntry.Id
        {
            get
            {
                return _id.Id;
            }
            set
            {
                if (_id.Id != Identifier.Empty)
                {
                    throw new InvalidOperationException("Unable to set Entry.Id. This property has already been assigned");
                }
                _id = new IdentifierComponent(value);
            }
        }

        IdentifierComponent IComponentEditableEntry.IdComponent
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}
