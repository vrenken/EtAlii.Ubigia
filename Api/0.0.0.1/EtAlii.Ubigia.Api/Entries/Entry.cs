namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    [DebuggerStepThrough]
    [DebuggerDisplay("{Type} ({Id.Era}.{Id.Period}.{Id.Moment})")]
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
            _id = new IdentifierComponent { Id = id };
        }

        private Entry(Identifier id, Relation previous)
            : this(id)
        {
            _previous = new PreviousComponent { Relation = previous };
        }

        public Entry(Identifier id, Relation previous, object content)
            : this(id)
        {
            _previous = new PreviousComponent { Relation = previous };
        }

        public Identifier Id { get { return _id.Id; } }
        private IdentifierComponent _id;

        Identifier IEditableEntry.Id
        {
            [DebuggerStepThrough]
            get { return _id.Id; }
            set
            {
                if (_id.Id != Identifier.Empty)
                {
                    throw new InvalidOperationException("Unable to set Entry.Id. This property has already been assigned");
                }
                _id = new IdentifierComponent { Id = value };
            }
        }

        IdentifierComponent IComponentEditableEntry.IdComponent
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
