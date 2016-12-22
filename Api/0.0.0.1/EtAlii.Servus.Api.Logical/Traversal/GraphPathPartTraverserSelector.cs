namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.xTechnology.Structure;

    internal class GraphPathPartTraverserSelector : Selector<GraphPathPart, IGraphPathPartTraverser>, IGraphPathPartTraverserSelector
    {
        public GraphPathPartTraverserSelector(
            IGraphPathNodeTraverser graphPathNodeTraverser,
            IGraphPathIdentifiersStartNodeTraverser graphPathIdentifiersStartNodeTraverser,
            IGraphPathRootStartNodeTraverser graphPathRootStartNodeTraverser,
            IGraphPathChildRelationTraverser graphPathChildRelationTraverser,
            IGraphPathParentRelationTraverser graphPathParentRelationTraverser,
            IGraphPathNextRelationTraverser graphPathNextRelationTraverser,
            IGraphPathPreviousRelationTraverser graphPathPreviousRelationTraverser,
            IGraphPathUpdateRelationTraverser graphPathUpdateRelationTraverser,
            IGraphPathDowndateRelationTraverser graphPathDowndateRelationTraverser,
            IGraphPathFinalRelationTraverser graphPathFinalRelationTraverser,
            IGraphPathOriginalRelationTraverser graphPathOriginalRelationTraverser,
            IGraphPathWildcardTraverser graphPathWildcardTraverser,
            ITraversingGraphPathWildcardTraverser traversingGraphPathWildcardTraverser,
            IGraphPathConditionalTraverser graphPathConditionalTraverser)
        {
            this.Register(part => part is GraphNode, graphPathNodeTraverser)
                .Register(part => part is GraphIdentifiersStartNode, graphPathIdentifiersStartNodeTraverser)
                .Register(part => part is GraphRootStartNode, graphPathRootStartNodeTraverser)
                .Register(part => part == GraphRelation.Child, graphPathChildRelationTraverser)
                .Register(part => part == GraphRelation.Parent, graphPathParentRelationTraverser)
                .Register(part => part == GraphRelation.Next, graphPathNextRelationTraverser)
                .Register(part => part == GraphRelation.Previous, graphPathPreviousRelationTraverser)
                .Register(part => part == GraphRelation.Update, graphPathUpdateRelationTraverser)
                .Register(part => part == GraphRelation.Downdate, graphPathDowndateRelationTraverser)
                .Register(part => part == GraphRelation.Final, graphPathFinalRelationTraverser)
                .Register(part => part == GraphRelation.Original, graphPathOriginalRelationTraverser)
                .Register(part => part is GraphWildcard, graphPathWildcardTraverser)
                .Register(part => part is GraphTraversingWildcard, traversingGraphPathWildcardTraverser)
                .Register(part => part is GraphCondition, graphPathConditionalTraverser);
        }
    }
}
