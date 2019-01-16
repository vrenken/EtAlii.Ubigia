namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.Structure;

    internal class GraphPathPartTraverserSelector : Selector<GraphPathPart, IGraphPathPartTraverser>, IGraphPathPartTraverserSelector
    {
        public GraphPathPartTraverserSelector(
            IGraphPathNodeTraverser graphPathNodeTraverser,
            IGraphPathIdentifiersStartNodeTraverser graphPathIdentifiersStartNodeTraverser,
            IGraphPathRootStartNodeTraverser graphPathRootStartNodeTraverser,
            IGraphPathAllChildrenRelationTraverser graphPathAllChildrenRelationTraverser,
            IGraphPathChildrenRelationTraverser graphPathChildrenRelationTraverser,
            IGraphPathAllParentsRelationTraverser graphPathAllParentsRelationTraverser,
            IGraphPathParentRelationTraverser graphPathParentRelationTraverser,
            IGraphPathAllNextRelationTraverser graphPathAllNextRelationTraverser,
            IGraphPathNextRelationTraverser graphPathNextRelationTraverser,
            IGraphPathAllPreviousRelationTraverser graphPathAllPreviousRelationTraverser,
            IGraphPathPreviousRelationTraverser graphPathPreviousRelationTraverser,
            IGraphPathAllUpdatesRelationTraverser graphPathAllUpdatesRelationTraverser,
            IGraphPathUpdatesRelationTraverser graphPathUpdatesRelationTraverser,
            IGraphPathAllDowndatesRelationTraverser graphPathAllDowndatesRelationTraverser,
            IGraphPathDowndateRelationTraverser graphPathDowndateRelationTraverser,
            IGraphPathFinalRelationTraverser graphPathFinalRelationTraverser,
            IGraphPathOriginalRelationTraverser graphPathOriginalRelationTraverser,
            IGraphPathWildcardTraverser graphPathWildcardTraverser,
            ITraversingGraphPathWildcardTraverser traversingGraphPathWildcardTraverser,
            IGraphPathConditionalTraverser graphPathConditionalTraverser)
        {
            Register(part => part is GraphNode, graphPathNodeTraverser)
                .Register(part => part is GraphIdentifiersStartNode, graphPathIdentifiersStartNodeTraverser)
                .Register(part => part is GraphRootStartNode, graphPathRootStartNodeTraverser)
                .Register(part => part == GraphRelation.AllChildren, graphPathAllChildrenRelationTraverser)
                .Register(part => part == GraphRelation.Children, graphPathChildrenRelationTraverser)
                .Register(part => part == GraphRelation.AllParents, graphPathAllParentsRelationTraverser)
                .Register(part => part == GraphRelation.Parent, graphPathParentRelationTraverser)
                .Register(part => part == GraphRelation.AllNext, graphPathAllNextRelationTraverser)
                .Register(part => part == GraphRelation.Next, graphPathNextRelationTraverser)
                .Register(part => part == GraphRelation.AllPrevious, graphPathAllPreviousRelationTraverser)
                .Register(part => part == GraphRelation.Previous, graphPathPreviousRelationTraverser)
                .Register(part => part == GraphRelation.AllUpdates, graphPathAllUpdatesRelationTraverser)
                .Register(part => part == GraphRelation.Updates, graphPathUpdatesRelationTraverser)
                .Register(part => part == GraphRelation.AllDowndates, graphPathAllDowndatesRelationTraverser)
                .Register(part => part == GraphRelation.Downdate, graphPathDowndateRelationTraverser)
                .Register(part => part == GraphRelation.Final, graphPathFinalRelationTraverser)
                .Register(part => part == GraphRelation.Original, graphPathOriginalRelationTraverser)
                .Register(part => part is GraphWildcard, graphPathWildcardTraverser)
                .Register(part => part is GraphTraversingWildcard, traversingGraphPathWildcardTraverser)
                .Register(part => part is GraphCondition, graphPathConditionalTraverser);
        }
    }
}
