namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class WildcardPathSubjectPartToGraphPathPartsConverter : IWildcardPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            var pattern = ((WildcardPathSubjectPart)pathSubjectPart).Pattern;

            var result = new GraphPathPart[] { new GraphWildcard(pattern) };
            return Task.FromResult(result);
        }
    }
}
