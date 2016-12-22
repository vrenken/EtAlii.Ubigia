namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphPathNextRelationTraverser : IGraphPathNextRelationTraverser
    {
        public GraphPathNextRelationTraverser()
        {
        }


        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.Subscribe(
                    onError: e => parameters.Output.OnError(e),
                    onNext: start =>
                    {
                        var task = Task.Run(async () =>
                        {
                            var entries = await parameters.Context.Entries.GetRelated(start, EntryRelation.Next, parameters.Scope);
                            var results = entries
                                .Select(e => e.Id)
                                .AsEnumerable();
                            foreach (var result in results)
                            {
                                parameters.Output.OnNext(result);
                            }
                        });
                        task.Wait();
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var entries = await context.Entries
                .GetRelated(start, EntryRelation.Next, scope);
            return entries
                .Select(e => e.Id)
                .AsEnumerable();
        }
    }
}