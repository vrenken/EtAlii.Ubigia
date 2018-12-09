namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class TraversingWildcardPathSubjectPartToGraphPathPartsConverter : ITraversingWildcardPathSubjectPartToGraphPathPartsConverter
    {
        private readonly IProcessingContext _context;

        public TraversingWildcardPathSubjectPartToGraphPathPartsConverter(IProcessingContext context)
        {
            _context = context;
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.Run(() =>
            {
                var limit = ((TraversingWildcardPathSubjectPart)pathSubjectPart).Limit;

                return new GraphPathPart[] { new GraphTraversingWildcard(limit) };
            });
        }
    }
}