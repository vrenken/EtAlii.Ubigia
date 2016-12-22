namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphPathFinalRelationTraverser : IGraphPathFinalRelationTraverser
    {
        public GraphPathFinalRelationTraverser()
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
                            Identifier[] results = new Identifier[] { start };
                            Identifier[] previousResults;

                            do
                            {
                                previousResults = results;
                                var nextResult = new List<Identifier>();
                                foreach (var result in results)
                                {
                                    var newResults = await parameters.Context.Entries.GetRelated(result, EntryRelation.Update, parameters.Scope);
                                    nextResult.AddRange(newResults.Select(e => e.Id));
                                }
                                results = nextResult.ToArray();
                            }
                            while (results.Any());

                            foreach (var previousResult in previousResults)
                            {
                                parameters.Output.OnNext(previousResult);
                            }
                        });
                        task.Wait();
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            Identifier[] result = new Identifier[] { start };
            Identifier[] previousResult;

            do
            {
                previousResult = result;
                var nextResult = new List<Identifier>();
                foreach (var r in result)
                {
                    var newResults = await context.Entries.GetRelated(r, EntryRelation.Update, scope);
                    nextResult.AddRange(newResults.Select(e => e.Id));
                }
                result = nextResult.ToArray();
            }
            while (result.Any());
            
            return previousResult;
        }
    }
}