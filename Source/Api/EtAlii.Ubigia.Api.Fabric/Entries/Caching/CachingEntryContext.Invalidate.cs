// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal partial class CachingEntryContext
    {
        private void InvalidateRelated(IReadOnlyEntry entry, ExecutionScope scope)
        {
            // We also want the following (forward oriented) items to be removed.
            Invalidate(entry.Children, scope);
            Invalidate(entry.Downdate, scope);
            Invalidate(entry.Indexed, scope);
            Invalidate(entry.Indexes, scope);
            Invalidate(entry.Parent, scope);
            Invalidate(entry.Previous, scope);
            Invalidate(entry.Updates, scope);
        }

        private void Invalidate(Identifier identifier, ExecutionScope scope)
        {
            if (scope.EntryCache.TryGetValue(identifier, out var entry))
            {
                // Yup, we got a cache hit.
                scope.EntryCache.Remove(identifier);

                InvalidateRelated(entry, scope);
            }
        }

        /// <summary>
        /// Use this method to invalidate a set of relations all at once.
        /// </summary>
        /// <param name="relations"></param>
        /// <param name="scope"></param>
        private void Invalidate(IEnumerable<Relation> relations, ExecutionScope scope)
        {
            foreach (var relation in relations)
            {
                Invalidate(relation.Id, scope);
            }
        }

        /// <summary>
        /// Invalidate a relation.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="scope"></param>
        private void Invalidate(Relation relation, ExecutionScope scope)
        {
            if (relation != Relation.None)
            {
                Invalidate(relation.Id, scope);
            }
        }
    }
}
