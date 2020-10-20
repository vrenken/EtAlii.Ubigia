namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    internal class GraphPathAllNextRelationTraverser : IGraphPathAllNextRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var entries = await parameters.Context.Entries
                            .GetRelated(start, EntryRelation.Next, parameters.Scope);
                        var results = entries
                            .Select(e => e.Id)
                            .AsEnumerable();
                        foreach (var result in results)
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var entries = await context.Entries
                .GetRelated(start, EntryRelation.Next, scope);
            var result = entries
                .Select(e => e.Id)
                .AsEnumerable();
            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}