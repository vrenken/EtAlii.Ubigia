namespace EtAlii.Servus.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    internal class UpdatePathSubjectPartToGraphPathPartsConverter : IUpdatePathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.FromResult(new GraphPathPart[] { GraphRelation.Update });
        }
    }
}