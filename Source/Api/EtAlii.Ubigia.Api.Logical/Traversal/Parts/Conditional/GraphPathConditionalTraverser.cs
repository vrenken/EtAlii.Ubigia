// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

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
                            var properties = await parameters.Context.Properties.Retrieve(start, parameters.Scope).ConfigureAwait(false);
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

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var predicate = ((GraphCondition)part).Predicate;

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Conditional traversal cannot be done at the root of a graph");
            }
            else
            {
                var properties = await context.Properties.Retrieve(start, scope).ConfigureAwait(false);
                if (properties != null)
                {
                    var shouldAdd = predicate(properties);
                    if (shouldAdd)
                    {
                        yield return start;
                    }
                }
            }
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
