// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.Linq;

public sealed partial class Entry
{
    public Relation Parent => ((IComponentEditableEntry) this).ParentComponent.Relation;

    public Relation[] Children { get; private set; }

    private readonly ChildrenComponentCollection _childrenComponent;

    IReadOnlyRelationsComponentCollection<ChildrenComponent> IEditableEntry.Children => _childrenComponent;

    IReadOnlyRelationsComponentCollection<ChildrenComponent> IComponentEditableEntry.ChildrenComponent => _childrenComponent;

    Relation IEditableEntry.Parent
    {
        get => ((IComponentEditableEntry) this).ParentComponent.Relation;
        set
        {
            if (((IComponentEditableEntry) this).ParentComponent.Relation != Relation.None)
            {
                throw new InvalidOperationException("Unable to set Entry.Parent. This relation has already been assigned");
            }
            ((IComponentEditableEntry) this).ParentComponent = new ParentComponent { Relation = value };
        }
    }

    ParentComponent IComponentEditableEntry.ParentComponent { get; set; }

    void IEditableEntry.AddChild(in Identifier id)
    {
        _childrenComponent.Add(id);
        Children = _childrenComponent
            .SelectMany(components => components.Relations)
            .ToArray();
    }

    void IComponentEditableEntry.AddChildren(Relation[] relations, bool markAsStored)
    {
        _childrenComponent.Add(relations, markAsStored);
        Children = _childrenComponent
            .SelectMany(components => components.Relations)
            .ToArray();
    }

}
