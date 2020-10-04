namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
                            var entry = await parameters.Context.Entries.Get(start, parameters.Scope);
                            if (regex.IsMatch(entry.Type))
                            {
                                parameters.Output.OnNext(entry.Id);
                            }
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();

            var pattern = ((GraphWildcard)part).Pattern;

            var regex = scope.GetWildCardRegex(pattern);

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Wildcard traversal cannot be done at the root of a graph");
            }
            else
            {
                var entry = await context.Entries.Get(start, scope);
                if (regex.IsMatch(entry.Type))
                {
                    result.Add(entry.Id);
                }
            }
            return result;
        }

    }
}