namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal abstract class RecursiveGraphPathTraverserBase  
    {
        public void Configure(TraversalParameters parameters)
        {
            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        if (start == Identifier.Empty)
                        {
                            throw new GraphTraversalException("Recursive traversal cannot be done at the root of a graph");
                        }

                        var results = TraverseRecursive(start, parameters.Context, parameters.Scope).Distinct();
                        await foreach (var result in results)
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Recursive traversal cannot be done at the root of a graph");
            }

            var result = TraverseRecursive(start, context, scope).Distinct();
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }
        }

        protected abstract IAsyncEnumerable<Identifier> GetNextRecursion(
            Identifier start, 
            IPathTraversalContext context,
            ExecutionScope scope); 

        private async IAsyncEnumerable<Identifier> TraverseRecursive(
            Identifier start, 
            IPathTraversalContext context,
            ExecutionScope scope)
        {
            yield return start;

            var subItems = GetNextRecursion(start, context, scope);

            await foreach (var subItem in subItems.ConfigureAwait(false))
            {
                var subResults = TraverseRecursive(subItem, context, scope);
                await foreach (var subResult in subResults)
                {
                    yield return subResult;
                }
            }
        }
    }
}