// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.Linq;

public sealed partial class Entry
{
    public Relation Downdate => ((IComponentEditableEntry) this).DowndateComponent.Relation;

    public Relation[] Updates { get; private set; }

    private readonly UpdatesComponentCollection _updatesComponent;

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

    IReadOnlyRelationsComponentCollection<UpdatesComponent> IEditableEntry.Updates => _updatesComponent;

    IReadOnlyRelationsComponentCollection<UpdatesComponent> IComponentEditableEntry.UpdatesComponent => _updatesComponent;


    void IEditableEntry.AddUpdate(in Identifier id)
    {
        _updatesComponent.Add(id);
        Updates = _updatesComponent
            .SelectMany(components => components.Relations)
            .ToArray();
    }

    void IComponentEditableEntry.AddUpdates(Relation[] relations, bool markAsStored)
    {
        _updatesComponent.Add(relations, markAsStored);
        Updates = _updatesComponent
            .SelectMany(components => components.Relations)
            .ToArray();
    }
}
