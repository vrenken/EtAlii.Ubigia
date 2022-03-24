// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class GraphPathFinalRelationTraverser : IGraphPathFinalRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var results = new[] { start };
                        Identifier[] previousResults;

                        do
                        {
                            previousResults = results;
                            var nextResult = new List<Identifier>();
                            foreach (var result in results)
                            {
                                var newResults = await parameters.Context.Entries
                                    .GetRelated(result, EntryRelations.Update, parameters.Scope)
                                    .Select(e => e.Id)
                                    .ToArrayAsync()
                                    .ConfigureAwait(false);
                                nextResult.AddRange(newResults);
                            }
                            results = nextResult.ToArray();
                        }
                        while (results.Any());

                        foreach (var previousResult in previousResults)
                        {
                            parameters.Output.OnNext(previousResult);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var result = new[] { start };
            Identifier[] previousResult;

            do
            {
                previousResult = result;
                var nextResult = new List<Identifier>();
                foreach (var r in result)
                {
                    var newResults = await context.Entries
                        .GetRelated(r, EntryRelations.Update, scope)
                        .Select(e => e.Id)
                        .ToArrayAsync()
                        .ConfigureAwait(false);
                    nextResult.AddRange(newResults);
                }
                result = nextResult.ToArray();
            }
            while (result.Any());

            // This feels fishy. Why would we not use the results and only the previous result?
            // This previous result is only being added once.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/72
            foreach (var item in previousResult)
            {
                yield return item;
            }
        }
    }
}
