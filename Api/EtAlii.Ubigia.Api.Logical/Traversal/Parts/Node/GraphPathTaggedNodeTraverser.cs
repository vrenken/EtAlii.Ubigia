namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GraphPathTaggedNodeTraverser : IGraphPathTaggedNodeTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var graphTaggedNode = (GraphTaggedNode) parameters.Part;
            var name = graphTaggedNode.Name;
            var tag = graphTaggedNode.Tag;

            parameters.Input.SubscribeAsync(
                onError: e => parameters.Output.OnError(e),
                onNext: async start =>
                {
                    if (start == Identifier.Empty)
                    {
                        throw new GraphTraversalException("Tagged node traversal cannot be done at the root of a graph");
                    }

                    var entry = await parameters.Context.Entries.Get(start, parameters.Scope);

                    if (name != string.Empty && name != entry.Type)
                    {
                        return;
                    }

                    if (tag != string.Empty && tag != entry.Tag)
                    {
                        return;
                    }

                    parameters.Output.OnNext(entry.Id);
                },
                onCompleted: () => parameters.Output.OnCompleted());
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();

            var graphTaggedNode = (GraphTaggedNode)part;
            var name = graphTaggedNode.Name;
            var tag = graphTaggedNode.Tag;

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Tagged node traversal cannot be done at the root of a graph");
            }
            var entry = await context.Entries.Get(start, scope);
            if (name != string.Empty && name != entry.Type)
            {
                return result;
            }
            if (tag != string.Empty && tag != entry.Tag)
            {
                return result;
            }
            result.Add(entry.Id);
            return result;
        }

    }
}