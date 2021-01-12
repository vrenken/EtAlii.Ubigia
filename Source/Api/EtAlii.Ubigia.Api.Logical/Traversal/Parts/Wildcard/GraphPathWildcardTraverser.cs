namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    internal class GraphPathWildcardTraverser : IGraphPathWildcardTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var pattern = ((GraphWildcard)parameters.Part).Pattern;

            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var regex = parameters.Scope.GetWildCardRegex(pattern);

                        if (start == Identifier.Empty)
                        {
                            throw new GraphTraversalException("Wildcard traversal cannot be done at the root of a graph");
                        }
                        else
                        {
                            var entry = await parameters.Context.Entries.Get(start, parameters.Scope).ConfigureAwait(false);
                            if (regex.IsMatch(entry.Type))
                            {
                                parameters.Output.OnNext(entry.Id);
                            }
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var pattern = ((GraphWildcard)part).Pattern;

            var regex = scope.GetWildCardRegex(pattern);

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Wildcard traversal cannot be done at the root of a graph");
            }
            else
            {
                var entry = await context.Entries.Get(start, scope).ConfigureAwait(false);
                if (regex.IsMatch(entry.Type))
                {
                    yield return entry.Id;
                }
            }
        }

    }
}