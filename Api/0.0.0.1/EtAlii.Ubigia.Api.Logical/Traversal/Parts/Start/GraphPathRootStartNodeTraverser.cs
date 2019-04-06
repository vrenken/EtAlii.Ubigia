namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GraphPathRootStartNodeTraverser : IGraphPathRootStartNodeTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var rootStartNode = (GraphRootStartNode)parameters.Part;
            var rootName = rootStartNode.Root;

            parameters.Input.SubscribeAsync(
                onError: e => parameters.Output.OnError(e),
                onNext: async o =>
                {
                    var root = await parameters.Context.Roots.Get(rootName);
                    parameters.Output.OnNext(root.Identifier);
                    parameters.Output.OnCompleted();
                },
                onCompleted: () => { });// parameters.Output.OnCompleted()});
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var rootStartNode = (GraphRootStartNode) part;
            var rootName = rootStartNode.Root;
            var root = await context.Roots.Get(rootName);
            return new[] { root.Identifier };
        }
    }
}