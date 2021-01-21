namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class TraversingGraphPathWildcardTraverser : ITraversingGraphPathWildcardTraverser
    {
        private readonly IGraphPathChildrenRelationTraverser _graphPathChildrenRelationTraverser;
        private readonly IGraphPathFinalRelationTraverser _graphPathFinalRelationTraverser;

        public TraversingGraphPathWildcardTraverser(
            IGraphPathChildrenRelationTraverser graphPathChildrenRelationTraverser, 
            IGraphPathFinalRelationTraverser graphPathFinalRelationTraverser)
        {
            _graphPathChildrenRelationTraverser = graphPathChildrenRelationTraverser;
            _graphPathFinalRelationTraverser = graphPathFinalRelationTraverser;
        }


        public void Configure(TraversalParameters parameters)
        {
            var limit = ((GraphTraversingWildcard)parameters.Part).Limit;

            parameters.Input.SubscribeAsync(
                    onError: e => parameters.Output.OnError(e),
                    onNext: async start =>
                    {
                        var results = new List<Identifier>();

                        if (limit == 0)
                        {
                            throw new NotSupportedException("not-limited wildcard traversal is not yet supported: The Traversers archictecture needs to be changed to allow for this.");
                        }

                        if (start == Identifier.Empty)
                        {
                            throw new GraphTraversalException("Wildcard traversal cannot be done at the root of a graph");
                        }
                        else
                        {
                            await TraverseChildren(results, start, parameters.Context, parameters.Scope, limit).ConfigureAwait(false); // EntryRelation.Child, 
                        }
                        foreach (var result in results.Distinct())
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var limit = ((GraphTraversingWildcard)part).Limit;
            if (limit == 0)
            {
                throw new NotSupportedException("not-limited wildcard traversal is not yet supported: The Traversers archictecture needs to be changed to allow for this.");
            }

            if (start == Identifier.Empty)
            {
                throw new GraphTraversalException("Wildcard traversal cannot be done at the root of a graph");
            }

            var result = new List<Identifier>();
            await TraverseChildren(result, start, context, scope, limit).ConfigureAwait(false); // EntryRelation.Child, 
            var results = result.Distinct();
            
            foreach (var item in results)
            {
                yield return item;
            }
        }

        private async Task TraverseChildren(
            ICollection<Identifier> result, 
            Identifier start, 
            IPathTraversalContext context,
            ExecutionScope scope, 
            //EntryRelation entryRelation, 
            int limit)
        {
            start = await _graphPathFinalRelationTraverser.Traverse(null, start, context, scope).SingleOrDefaultAsync().ConfigureAwait(false);
            result.Add(start);

            if (limit > 1)
            {
                var subItems = _graphPathChildrenRelationTraverser.Traverse(null, start, context, scope);

                await foreach (var subItem in subItems.ConfigureAwait(false))
                {
                    await TraverseChildren(result, subItem, context, scope, limit - 1).ConfigureAwait(false); // , entryRelation
                }
            }
        }
    }
}