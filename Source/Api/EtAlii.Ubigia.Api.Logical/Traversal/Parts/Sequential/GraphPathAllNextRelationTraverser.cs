// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphPathAllNextRelationTraverser : IGraphPathAllNextRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var results = parameters.Context.Entries
                            .GetRelated(start, EntryRelation.Next, parameters.Scope)
                            .Select(e => e.Id);
                        await foreach (var result in results.ConfigureAwait(false))
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            return context.Entries
                .GetRelated(start, EntryRelation.Next, scope)
                .Select(e => e.Id);
        }
    }
}
