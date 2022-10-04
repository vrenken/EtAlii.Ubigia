// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Linq;

    internal class HierarchicalRelationDuplicator : IHierarchicalRelationDuplicator
    {
        public void Duplicate(IReadOnlyEntry source, IEditableEntry target)
        {
            Duplicate(source, target, Identifier.Empty);
        }

        public void Duplicate(IReadOnlyEntry source, IEditableEntry target, in Identifier relationToExclude)
        {
            DuplicateRelations(source.Children, target.Children, relationToExclude);
            DuplicateRelations(source.Children2, target.Children2, relationToExclude);

            if (source.Parent != Relation.None)
            {
                target.Parent = Relation.NewRelation(source.Parent.Id);
            }

            if (source.Parent2 != Relation.None)
            {
                target.Parent2 = Relation.NewRelation(source.Parent2.Id);
            }
        }

        private void DuplicateRelations<TRelationsComponent>(Relation[] source, RelationsComponentCollection<TRelationsComponent> target, Identifier relationToExclude)
            where TRelationsComponent : RelationsComponent, new()
        {
            if (relationToExclude != Identifier.Empty)
            {
                source = source
                    .Where(relation => relation.Id != relationToExclude)
                    .ToArray();
            }
            target.Add(source, false);
        }

    }
}
