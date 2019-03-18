namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GraphPathTaggedNodeTraverser : IGraphPathTaggedNodeTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var graphTaggedNode = (GraphTaggedNode) parameters.Part;
            var name = graphTaggedNode.Name;
            var tag = graphTaggedNode.Tag;

            parameters.Input.Subscribe(
                    onError: e => parameters.Output.OnError(e),
                    onNext: start =>
                    {
                        var task = Task.Run(async () =>
                        {
                            if (start == Identifier.Empty)
                            {
                                throw new GraphTraversalException("Tagged node traversal cannot be done at the root of a graph");
                            }
                            else
                            {
                                var entry = await parameters.Context.Entries.Get(start, parameters.Scope);
                                if (entry.Type == name && entry.Tag == tag)
                                {
                                    parameters.Output.OnNext(entry.Id);
                                }
                            }
                        });
                        task.Wait();
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();

            var graphTaggedNode = (GraphTaggedNode)part;

            //var regex = scope.GetWildCardRegex(pattern);

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Tagged node traversal cannot be done at the root of a graph");
            }
            else
            {
                var entry = await context.Entries.Get(start, scope);
                if (graphTaggedNode.Name != String.Empty && graphTaggedNode.Name != entry.Type)
                {
                    return result;
                }
                if (graphTaggedNode.Tag != String.Empty && graphTaggedNode.Tag == entry.Tag)
                {
                    return result;
                }
                result.Add(entry.Id);
            }
            return result;
        }

    }
}