namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    internal class GraphPathPartTraverserSelector : IGraphPathPartTraverserSelector
    {
        private readonly IGraphPathNodeTraverser _graphPathNodeTraverser;
        private readonly IGraphPathIdentifiersStartNodeTraverser _graphPathIdentifiersStartNodeTraverser;
        private readonly IGraphPathRootStartNodeTraverser _graphPathRootStartNodeTraverser;
        private readonly IGraphPathAllChildrenRelationTraverser _graphPathAllChildrenRelationTraverser;
        private readonly IGraphPathChildrenRelationTraverser _graphPathChildrenRelationTraverser;
        private readonly IGraphPathAllParentsRelationTraverser _graphPathAllParentsRelationTraverser;
        private readonly IGraphPathParentRelationTraverser _graphPathParentRelationTraverser;
        private readonly IGraphPathAllNextRelationTraverser _graphPathAllNextRelationTraverser;
        private readonly IGraphPathNextRelationTraverser _graphPathNextRelationTraverser;
        private readonly IGraphPathAllPreviousRelationTraverser _graphPathAllPreviousRelationTraverser;
        private readonly IGraphPathPreviousRelationTraverser _graphPathPreviousRelationTraverser;
        private readonly IGraphPathAllUpdatesRelationTraverser _graphPathAllUpdatesRelationTraverser;
        private readonly IGraphPathUpdatesRelationTraverser _graphPathUpdatesRelationTraverser;
        private readonly IGraphPathAllDowndatesRelationTraverser _graphPathAllDowndatesRelationTraverser;
        private readonly IGraphPathDowndateRelationTraverser _graphPathDowndateRelationTraverser;
        private readonly IGraphPathFinalRelationTraverser _graphPathFinalRelationTraverser;
        private readonly IGraphPathOriginalRelationTraverser _graphPathOriginalRelationTraverser;
        private readonly IGraphPathTaggedNodeTraverser _graphPathTaggedNodeTraverser;
        private readonly IGraphPathWildcardTraverser _graphPathWildcardTraverser;
        private readonly ITraversingGraphPathWildcardTraverser _traversingGraphPathWildcardTraverser;
        private readonly IGraphPathConditionalTraverser _graphPathConditionalTraverser;

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
            IGraphPathTaggedNodeTraverser graphPathTaggedNodeTraverser,
            IGraphPathWildcardTraverser graphPathWildcardTraverser,
            ITraversingGraphPathWildcardTraverser traversingGraphPathWildcardTraverser,
            IGraphPathConditionalTraverser graphPathConditionalTraverser)
        {
            _graphPathNodeTraverser = graphPathNodeTraverser;
            _graphPathIdentifiersStartNodeTraverser = graphPathIdentifiersStartNodeTraverser;
            _graphPathRootStartNodeTraverser = graphPathRootStartNodeTraverser;
            _graphPathAllChildrenRelationTraverser = graphPathAllChildrenRelationTraverser;
            _graphPathChildrenRelationTraverser = graphPathChildrenRelationTraverser;
            _graphPathAllParentsRelationTraverser = graphPathAllParentsRelationTraverser;
            _graphPathParentRelationTraverser = graphPathParentRelationTraverser;
            _graphPathAllNextRelationTraverser = graphPathAllNextRelationTraverser;
            _graphPathNextRelationTraverser = graphPathNextRelationTraverser;
            _graphPathAllPreviousRelationTraverser = graphPathAllPreviousRelationTraverser;
            _graphPathPreviousRelationTraverser = graphPathPreviousRelationTraverser;
            _graphPathAllUpdatesRelationTraverser = graphPathAllUpdatesRelationTraverser;
            _graphPathUpdatesRelationTraverser = graphPathUpdatesRelationTraverser;
            _graphPathAllDowndatesRelationTraverser = graphPathAllDowndatesRelationTraverser;
            _graphPathDowndateRelationTraverser = graphPathDowndateRelationTraverser;
            _graphPathFinalRelationTraverser = graphPathFinalRelationTraverser;
            _graphPathOriginalRelationTraverser = graphPathOriginalRelationTraverser;
            _graphPathTaggedNodeTraverser = graphPathTaggedNodeTraverser;
            _graphPathWildcardTraverser = graphPathWildcardTraverser;
            _traversingGraphPathWildcardTraverser = traversingGraphPathWildcardTraverser;
            _graphPathConditionalTraverser = graphPathConditionalTraverser;
        }

        public IGraphPathPartTraverser Select(GraphPathPart part)
        {
            return part switch
            {
                GraphNode => _graphPathNodeTraverser,
                GraphIdentifiersStartNode => _graphPathIdentifiersStartNodeTraverser,
                GraphRootStartNode => _graphPathRootStartNodeTraverser,
                { } when part == GraphRelation.AllChildren => _graphPathAllChildrenRelationTraverser,
                { } when part == GraphRelation.Children => _graphPathChildrenRelationTraverser,
                { } when part == GraphRelation.AllParents => _graphPathAllParentsRelationTraverser,
                { } when part == GraphRelation.Parent => _graphPathParentRelationTraverser,
                { } when part == GraphRelation.AllNext => _graphPathAllNextRelationTraverser,
                { } when part == GraphRelation.Next => _graphPathNextRelationTraverser,
                { } when part == GraphRelation.AllPrevious => _graphPathAllPreviousRelationTraverser,
                { } when part == GraphRelation.Previous => _graphPathPreviousRelationTraverser,
                { } when part == GraphRelation.AllUpdates => _graphPathAllUpdatesRelationTraverser,
                { } when part == GraphRelation.Updates => _graphPathUpdatesRelationTraverser,
                { } when part == GraphRelation.AllDowndates => _graphPathAllDowndatesRelationTraverser,
                { } when part == GraphRelation.Downdate => _graphPathDowndateRelationTraverser,
                { } when part == GraphRelation.Final => _graphPathFinalRelationTraverser,
                { } when part == GraphRelation.Original => _graphPathOriginalRelationTraverser,
                { } when part is GraphTaggedNode => _graphPathTaggedNodeTraverser,
                { } when part is GraphWildcard => _graphPathWildcardTraverser,
                { } when part is GraphTraversingWildcard => _traversingGraphPathWildcardTraverser,
                { } when part is GraphCondition => _graphPathConditionalTraverser,
                _ => throw new NotSupportedException($"Unable to select traverser for graph path part {part?.ToString() ?? "NULL"}")
            };
        }
    }
}
