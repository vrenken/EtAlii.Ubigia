// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public sealed class BreadthFirstTraversalAlgorithm : IBreadthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

        public BreadthFirstTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
        }
        public async IAsyncEnumerable<Identifier> Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope)
        {
            IEnumerable<Identifier> currentResult = new[] { current };

            for (var i = 0; i < graphPath.Length; i++)
            {
                var currentGraphPathPart = graphPath[i];

                var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart);

                var iterationResult = new List<Identifier>();

                var isLast = i == graphPath.Length - 1;
                foreach (var identifier in currentResult)
                {
                    var relatedNodes = traverser
                        .Traverse(currentGraphPathPart, identifier, context, scope)
                        .ConfigureAwait(false);

                    var results = HandleCurrentResult(relatedNodes, isLast, iterationResult)
                        .ConfigureAwait(false);
                    await foreach (var result in results)
                    {
                        yield return result;
                    }
                }
                if (!isLast)
                {
                    currentResult = iterationResult;
                }
            }
        }

        private async IAsyncEnumerable<Identifier> HandleCurrentResult(ConfiguredCancelableAsyncEnumerable<Identifier> relatedNodes, bool isLast, List<Identifier> iterationResult)
        {

            if (isLast)
            {
                await foreach (var relatedNode in relatedNodes)
                {
                    yield return relatedNode;
                }
            }
            else
            {
                await foreach (var relatedNode in relatedNodes)
                {
                    iterationResult.Add(relatedNode);
                }
            }
        }
    }
}
