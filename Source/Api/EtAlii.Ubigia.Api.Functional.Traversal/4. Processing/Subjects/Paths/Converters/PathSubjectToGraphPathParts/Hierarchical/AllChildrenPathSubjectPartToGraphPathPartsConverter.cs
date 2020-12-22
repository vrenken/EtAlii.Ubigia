namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AllChildrenPathSubjectPartToGraphPathPartsConverter : IAllChildrenPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.FromResult(pathSubjectPartPosition == 0 ? Array.Empty<GraphPathPart>() : new GraphPathPart[] { GraphRelation.AllParents });
        }
    }
}
