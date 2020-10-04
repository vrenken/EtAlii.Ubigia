namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphPathUpdatesRelationTraverser : IGraphPathUpdatesRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var entries = await parameters.Context.Entries.GetRelated(start, EntryRelation.Update, parameters.Scope);
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

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var entries = await context.Entries
                .GetRelated(start, EntryRelation.Update, scope);
            return entries
                .Select(e => e.Id)
                .AsEnumerable();
        }
    }
}