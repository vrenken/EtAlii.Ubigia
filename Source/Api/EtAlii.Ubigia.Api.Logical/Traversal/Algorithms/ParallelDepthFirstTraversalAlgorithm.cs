namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public class ParallelDepthFirstTraversalAlgorithm : IDepthFirstTraversalAlgorithm
    {
        //private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector
        //private readonly int _maxDegreeOfParallelism
        //private readonly int ProcessorMultiplier = 2

        public Task Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
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
}
