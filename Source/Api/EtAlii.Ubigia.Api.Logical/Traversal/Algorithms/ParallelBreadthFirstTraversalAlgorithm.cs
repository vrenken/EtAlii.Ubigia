// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Parallel = EtAlii.xTechnology.Threading.Parallel;

    /// <summary>
    /// This class uses breadth first parallelization to achieve higher throughput.
    /// </summary>
    public class ParallelBreadthFirstTraversalAlgorithm : IBreadthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;
        private readonly int _maxDegreeOfParallelism;
        private readonly int ProcessorMultiplier = 2;

        public ParallelBreadthFirstTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
            _maxDegreeOfParallelism = Environment.ProcessorCount * ProcessorMultiplier;
        }

        public async Task Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
        {
            var previousResult = new[] { current };

            for (var i = 0; i < graphPath.Length; i++)
            {
                var currentGraphPathPart = graphPath[i];

                var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart);


                var resultCount = previousResult.Length;
                var subResults = new List<Identifier>[resultCount];

                await Parallel.ForAsync(previousResult, _maxDegreeOfParallelism, async (identifier, index) =>
                {
                    var list = new List<Identifier>();

                    var results = traverser.Traverse(currentGraphPathPart, identifier, context, scope);
                    await foreach (var result in results.ConfigureAwait(false))
                    {
                        list.Add(result);
                    }
                    subResults[index] = list;
                }).ConfigureAwait(false);

                var iterationResult = subResults.SelectMany(sr => sr);

                if (i == graphPath.Length - 1)
                {
                    foreach (var identifier in iterationResult)
                    {
                        finalOutput.OnNext(identifier);
                    }
                }
                else
                {
                    previousResult = iterationResult.ToArray();
                }
            }
        }

    }
}
