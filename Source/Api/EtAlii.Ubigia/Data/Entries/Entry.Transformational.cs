// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Downdate => ((IComponentEditableEntry) this).DowndateComponent.Relation;

        public IEnumerable<Relation> Updates => _updates.SelectMany(components => components.Relations);
        private readonly UpdatesComponentCollection _updates;

        Relation IEditableEntry.Downdate
        {
            get => ((IComponentEditableEntry) this).DowndateComponent.Relation;
            set
            {
                if (((IComponentEditableEntry) this).DowndateComponent.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Downdate. This relation has already been assigned");
                }
                ((IComponentEditableEntry) this).DowndateComponent = new DowndateComponent { Relation = value };
            }
        }

        DowndateComponent IComponentEditableEntry.DowndateComponent { get; set; }

        UpdatesComponentCollection IEditableEntry.Updates => _updates;

        UpdatesComponentCollection IComponentEditableEntry.UpdatesComponent => _updates;
    }
}
