namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class UpdatesPathSubjectPartToGraphPathPartsConverter : IUpdatesPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.FromResult(new GraphPathPart[] { GraphRelation.Updates });
        }
    }
}