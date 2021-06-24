// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    internal class GraphPathAllUpdatesRelationTraverser : RecursiveGraphPathTraverserBase, IGraphPathAllUpdatesRelationTraverser
    {
        protected override IAsyncEnumerable<Identifier> GetNextRecursion(Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            return context.Entries
                .GetRelated(start, EntryRelation.Update, scope)
                .Select(e => e.Id);
        }
    }
}