// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    internal class GraphPathAllDowndatesRelationTraverser : RecursiveGraphPathTraverserBase, IGraphPathAllDowndatesRelationTraverser
    {
//        public GraphPathAllDowndatesRelationTraverser(IGraphPathFinalRelationTraverser graphPathFinalRelationTraverser)
//            : base(graphPathFinalRelationTraverser)
//        [
//        ]
//
        protected override IAsyncEnumerable<Identifier> GetNextRecursion(Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            return context.Entries
                .GetRelated(start, EntryRelation.Downdate, scope)
                .Select(e => e.Id);
        }
    }
}
