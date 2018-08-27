namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class ParentPathSubjectPartToGraphPathPartsConverter : IParentPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.FromResult(pathSubjectPartPosition == 0 ? new GraphPathPart[] { } : new GraphPathPart[] { GraphRelation.Children });
        }
    }
}