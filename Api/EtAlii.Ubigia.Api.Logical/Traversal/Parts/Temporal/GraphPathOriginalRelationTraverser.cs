namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    internal class GraphPathOriginalRelationTraverser : IGraphPathOriginalRelationTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        Relation downDate;
                        Identifier previousResult;
                        var result = start;
                        do
                        {
                            previousResult = result;
                            downDate = (await parameters.Context.Entries.Get(previousResult, parameters.Scope)).Downdate;
                            result = downDate.Id;
                        }
                        while (downDate != Relation.None);

                        parameters.Output.OnNext(previousResult);
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            Relation downDate;
            Identifier previousResult;
            var result = start;
            do
            {
                previousResult = result;
                downDate = (await context.Entries.Get(previousResult, scope)).Downdate;
                result = downDate.Id;
            }
            while (downDate != Relation.None);

            yield return previousResult;
        }
    }
}