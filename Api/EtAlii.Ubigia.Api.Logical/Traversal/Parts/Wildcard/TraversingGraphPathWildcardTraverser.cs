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
                            await TraverseChildren(results, start, parameters.Context, parameters.Scope, limit); // EntryRelation.Child, 
                        }
                        foreach (var result in results.Distinct())
                        {
                            parameters.Output.OnNext(result);
                        }
                    },
                    onCompleted: () => parameters.Output.OnCompleted());
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var result = new List<Identifier>();

            var limit  = ((GraphTraversingWildcard)part).Limit;
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
                await TraverseChildren(result, start, context, scope, limit); // EntryRelation.Child, 
            }
            return result.Distinct();
        }

        private async Task TraverseChildren(
            ICollection<Identifier> result, 
            Identifier start, 
            ITraversalContext context,
            ExecutionScope scope, 
            //EntryRelation entryRelation, 
            int limit)
        {
            start = (await _graphPathFinalRelationTraverser.Traverse(null, start, context, scope)).SingleOrDefault();
            result.Add(start);

            if (limit > 1)
            {
                var subItems = (await _graphPathChildrenRelationTraverser.Traverse(null, start, context, scope))
                    .ToArray();

                foreach (var subItem in subItems)
                {
                    await TraverseChildren(result, subItem, context, scope, limit - 1); // , entryRelation
                }
            }
        }
    }
}