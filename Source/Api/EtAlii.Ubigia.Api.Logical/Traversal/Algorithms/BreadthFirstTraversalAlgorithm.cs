// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class BreadthFirstTraversalAlgorithm : IBreadthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

        public BreadthFirstTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
        }
        public async Task Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
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

                    await HandleCurrentResult(relatedNodes, finalOutput, isLast, iterationResult)
                        .ConfigureAwait(false);
                }
                if (!isLast)
                {
                    currentResult = iterationResult;
                }
            }
        }

        private async Task HandleCurrentResult(
            ConfiguredCancelableAsyncEnumerable<Identifier> relatedNodes,
            IObserver<Identifier> finalOutput,
            bool isLast,
            List<Identifier> iterationResult)
        {

            if (isLast)
            {
                await foreach (var relatedNode in relatedNodes)
                {
                    finalOutput.OnNext(relatedNode);
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
