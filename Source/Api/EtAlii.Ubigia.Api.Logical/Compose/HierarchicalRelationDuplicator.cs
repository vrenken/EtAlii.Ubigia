// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Linq;

internal class HierarchicalRelationDuplicator : IHierarchicalRelationDuplicator
{
    public void Duplicate(IReadOnlyEntry source, IEditableEntry target)
    {
        Duplicate(source, target, Identifier.Empty);
    }

    public void Duplicate(IReadOnlyEntry source, IEditableEntry target, in Identifier relationToExclude)
    {
        var children = FindRelationsToDuplicate(source.Children, relationToExclude);
        ((IComponentEditableEntry)target).AddChildren(children, false);
        var children2 = FindRelationsToDuplicate(source.Children2, relationToExclude);
        ((IComponentEditableEntry)target).AddChildren2(children2, false);

        if (source.Parent != Relation.None)
        {
            target.Parent = Relation.NewRelation(source.Parent.Id);
        }

        if (source.Parent2 != Relation.None)
        {
            target.Parent2 = Relation.NewRelation(source.Parent2.Id);
        }
    }

    private Relation[] FindRelationsToDuplicate(
        Relation[] relations,
        Identifier relationToExclude)
    {
        if (relationToExclude != Identifier.Empty)
        {
            relations = relations
                .Where(relation => relation.Id != relationToExclude)
                .ToArray();
        }
        return relations;
    }

}
