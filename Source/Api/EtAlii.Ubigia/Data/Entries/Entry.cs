// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{Type} ({Id.Era}.{Id.Period}.{Id.Moment})")]
    public sealed partial class Entry : IEquatable<Entry>, IReadOnlyEntry, IEditableEntry, IComponentEditableEntry
    {
        private Entry()
        {
            // Temporal
            ((IComponentEditableEntry) this).DowndateComponent = new DowndateComponent();
            _updates = new UpdatesComponentCollection();

            // Hierarchical
            ((IComponentEditableEntry) this).ParentComponent = new ParentComponent();
            _children = new ChildrenComponentCollection();

            // Hierarchical
            ((IComponentEditableEntry) this).Parent2Component = new Parent2Component();
            _children2 = new Children2ComponentCollection();

            // Indexed
            _indexes = new IndexesComponentCollection();
            ((IComponentEditableEntry) this).IndexedComponent = new IndexedComponent();

            // Sequential
            ((IComponentEditableEntry) this).PreviousComponent = new PreviousComponent();
            ((IComponentEditableEntry) this).NextComponent = new NextComponent();

            ((IComponentEditableEntry)this).TypeComponent = new TypeComponent();
            ((IComponentEditableEntry)this).TagComponent = new TagComponent();
            ((IComponentEditableEntry) this).IdComponent = new IdentifierComponent();
        }

        private Entry(in Identifier id)
            : this()
        {
            ((IComponentEditableEntry) this).IdComponent = new IdentifierComponent { Id = id };
        }

#pragma warning disable S1144 // SonarQube does not seem to understand the new C#9 new() constructor.
        private Entry(in Identifier id, Relation previous)
            : this(id)
        {
            ((IComponentEditableEntry) this).PreviousComponent = new PreviousComponent { Relation = previous };
        }
#pragma warning restore S1144

        public Identifier Id => ((IComponentEditableEntry) this).IdComponent.Id;

        Identifier IEditableEntry.Id
        {
            [DebuggerStepThrough]
            get => ((IComponentEditableEntry) this).IdComponent.Id;
            set
            {
                if (((IComponentEditableEntry) this).IdComponent.Id != Identifier.Empty)
                {
                    throw new InvalidOperationException("Unable to set Entry.Id. This property has already been assigned");
                }
                ((IComponentEditableEntry) this).IdComponent = new IdentifierComponent { Id = value };
            }
        }

        IdentifierComponent IComponentEditableEntry.IdComponent { get; set; }
    }
}
