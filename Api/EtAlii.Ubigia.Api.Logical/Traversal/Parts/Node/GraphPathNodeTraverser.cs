namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GraphPathNodeTraverser : IGraphPathNodeTraverser
    {
        public void Configure(TraversalParameters parameters)
        {
            var name = ((GraphNode)parameters.Part).Name;

            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        if (start == Identifier.Empty)
                        {
                            var root = await parameters.Context.Roots.Get(name);
                            parameters.Output.OnNext(root.Identifier);
                        }
                        else
                        {
                            var entry = await parameters.Context.Entries.Get(start, parameters.Scope);
                            if (entry.Type == name)
                            {
                                parameters.Output.OnNext(entry.Id);
                            }
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());

        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();

            var name = ((GraphNode) part).Name;

            if (start == Identifier.Empty)
            {
                var root = await context.Roots.Get(name);
                result.Add(root.Identifier);
            }
            else
            {
                var entry = await context.Entries.Get(start, scope);
                if (entry.Type == name)
                {
                    result.Add(entry.Id);
                }
            }
            return result;
        }
    }
}