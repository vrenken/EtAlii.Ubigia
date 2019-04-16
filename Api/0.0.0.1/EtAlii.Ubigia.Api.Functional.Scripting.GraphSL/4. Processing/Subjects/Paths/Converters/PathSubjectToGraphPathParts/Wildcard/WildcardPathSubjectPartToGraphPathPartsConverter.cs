namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class WildcardPathSubjectPartToGraphPathPartsConverter : IWildcardPathSubjectPartToGraphPathPartsConverter
    {
        private readonly IProcessingContext _context;

        public WildcardPathSubjectPartToGraphPathPartsConverter(IProcessingContext context)
        {
            _context = context;
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            var pattern = ((WildcardPathSubjectPart)pathSubjectPart).Pattern;

            var result = new GraphPathPart[] { new GraphWildcard(pattern) };
            return Task.FromResult(result);
        }
    }
}