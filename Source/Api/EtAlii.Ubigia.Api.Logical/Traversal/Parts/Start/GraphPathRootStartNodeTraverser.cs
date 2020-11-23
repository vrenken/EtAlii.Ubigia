namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    internal class GraphPathRootStartNodeTraverser : IGraphPathRootStartNodeTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var rootStartNode = (GraphRootStartNode)parameters.Part;
            var rootName = rootStartNode.Root;

            parameters.Input.SubscribeAsync(
                onError: e => parameters.Output.OnError(e),
                onNext: async _ =>
                {
                    var root = await parameters.Context.Roots.Get(rootName).ConfigureAwait(false);
                    parameters.Output.OnNext(root.Identifier);
                    parameters.Output.OnCompleted();
                },
                onCompleted: () => { });// parameters.Output.OnCompleted()])
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var rootStartNode = (GraphRootStartNode) part;
            var rootName = rootStartNode.Root;
            var root = await context.Roots.Get(rootName).ConfigureAwait(false);

            yield return root.Identifier;
        }
    }
}