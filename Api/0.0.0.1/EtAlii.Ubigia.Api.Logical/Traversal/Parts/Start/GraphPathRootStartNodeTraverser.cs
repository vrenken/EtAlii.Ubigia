namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GraphPathRootStartNodeTraverser : IGraphPathRootStartNodeTraverser
    {
        public GraphPathRootStartNodeTraverser()
        {
        }


        public void Configure(TraversalParameters parameters)
        {
            var rootStartNode = (GraphRootStartNode)parameters.Part;
            var rootName = rootStartNode.Root;

            parameters.Input.Subscribe(
                onError: e => parameters.Output.OnError(e),
                onNext: o =>
                {
                    var task = Task.Run(async () =>
                    {
                        var root = await parameters.Context.Roots.Get(rootName);
                        parameters.Output.OnNext(root.Identifier);
                        parameters.Output.OnCompleted();
                    });
                    task.Wait();
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