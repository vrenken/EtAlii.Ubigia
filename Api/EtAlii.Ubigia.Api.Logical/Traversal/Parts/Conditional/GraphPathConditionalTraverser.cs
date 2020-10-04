namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    internal class GraphPathConditionalTraverser : IGraphPathConditionalTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var predicate = ((GraphCondition)parameters.Part).Predicate;

            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        if (start == Identifier.Empty)
                        {
                            throw new GraphTraversalException("Conditional traversal cannot be done at the root of a graph");
                        }
                        else
                        {
                            var properties = await parameters.Context.Properties.Retrieve(start, parameters.Scope);
                            if (properties != null)
                            {
                                var shouldAdd = predicate(properties);
                                if (shouldAdd)
                                {
                                    parameters.Output.OnNext(start);
                                }
                            }
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();

            var predicate = ((GraphCondition)part).Predicate;

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Conditional traversal cannot be done at the root of a graph");
            }
            else
            {
                var properties = await context.Properties.Retrieve(start, scope);
                if (properties != null)
                {
                    var shouldAdd = predicate(properties);
                    if (shouldAdd)
                    {
                        result.Add(start);
                    }
                }
            }
            return result;
        }

        public string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                              .Replace(@"\*", ".*")
                              .Replace(@"\?", ".")
                       + "$";
        }
    }
}