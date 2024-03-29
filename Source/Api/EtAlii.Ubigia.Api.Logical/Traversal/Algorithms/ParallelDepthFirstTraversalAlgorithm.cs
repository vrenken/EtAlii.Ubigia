﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Collections.Generic;

public sealed class ParallelDepthFirstTraversalAlgorithm : IDepthFirstTraversalAlgorithm
{
    //private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector
    //private readonly int _maxDegreeOfParallelism
    //private readonly int ProcessorMultiplier = 2

    public IAsyncEnumerable<Identifier> Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope)
    {
        throw new NotSupportedException();
        //if [graphPath.Any[]]
        //[
        //    var currentGraphPathPart = graphPath.First()
        //    var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart)

        //    var relatedNodes = (await traverser.Traverse(currentGraphPathPart, current, context, scope))
        //        .ToArray()

        //    var subPathParts = graphPath.Skip(1).ToArray()
        //    if [subPathParts.Any[]]
        //    [
        //        var resultCount = relatedNodes.Length
        //        var subResults = new List<Identifier>[resultCount]

        //        await Parallel.ForAsync(relatedNodes, _maxDegreeOfParallelism, async (identifier, index) =>
        //        [
        //            var subResult = new List<Identifier>()
        //            var subGraphPath = new GraphPath(subPathParts)
        //            await Traverse(subGraphPath, identifier, subResult, context, scope)
        //            subResults[index] = subResult
        //        ])

        //        result.AddRange(subResults.SelectMany(sr => sr))
        //    ]
        //    else
        //    [
        //        result.AddRangeOnce(relatedNodes)
        //    ]
        //]
    }
}
