// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public sealed partial class Entry
    {
        public Relation Previous => ((IComponentEditableEntry) this).PreviousComponent.Relation;

        public Relation Next => ((IComponentEditableEntry) this).NextComponent.Relation;

        Relation IEditableEntry.Previous
        {
            get => ((IComponentEditableEntry) this).PreviousComponent.Relation;
            set
            {
                if (((IComponentEditableEntry) this).PreviousComponent.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Previous. This relation has already been assigned");
                }
                ((IComponentEditableEntry) this).PreviousComponent = new PreviousComponent { Relation = value };
            }
        }

        PreviousComponent IComponentEditableEntry.PreviousComponent { get; set; }

        Relation IEditableEntry.Next
        {
            get => ((IComponentEditableEntry) this).NextComponent.Relation;
            set
            {
                if (((IComponentEditableEntry) this).NextComponent.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Next. This property has already been assigned");
                }
                ((IComponentEditableEntry) this).NextComponent = new NextComponent { Relation = value };
            }
        }

        NextComponent IComponentEditableEntry.NextComponent { get; set; }
    }
}
