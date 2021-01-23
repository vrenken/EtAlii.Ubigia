namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphPathAllPreviousRelationTraverser : IGraphPathAllPreviousRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var results = parameters.Context.Entries
                            .GetRelated(start, EntryRelation.Previous, parameters.Scope)
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
                .GetRelated(start, EntryRelation.Previous, scope)
                .Select(e => e.Id);
        }
    }
}
