namespace EtAlii.Servus.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    internal class IsChildOfPathSubjectPartToGraphPathPartsConverter : IIsChildOfPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.FromResult(pathSubjectPartPosition == 0 ? new GraphPathPart[] { } : new GraphPathPart[] { GraphRelation.Parent });
        }
    }
}