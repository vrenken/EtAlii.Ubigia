namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class TraversingWildcardPathSubjectPartToGraphPathPartsConverter : ITraversingWildcardPathSubjectPartToGraphPathPartsConverter
    {
        //private readonly IProcessingContext _context

        public TraversingWildcardPathSubjectPartToGraphPathPartsConverter()//IProcessingContext context
        {
            //_context = context
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            var limit = ((TraversingWildcardPathSubjectPart)pathSubjectPart).Limit;

            var result = new GraphPathPart[] { new GraphTraversingWildcard(limit) };
            return Task.FromResult(result);
        }
    }
}