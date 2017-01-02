namespace EtAlii.Servus.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    internal class ConstantPathSubjectPartToGraphPathPartsConverter : IConstantPathSubjectPartToGraphPathPartsConverter
    {
        private readonly IProcessingContext _context;

        public ConstantPathSubjectPartToGraphPathPartsConverter(IProcessingContext context)
        {
            _context = context;
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.Run(() =>
            {
                GraphPathPart[] result = { null };
                var name = ((ConstantPathSubjectPart)pathSubjectPart).Name;

                if (pathSubjectPartPosition == 0 ||
                    pathSubjectPartPosition == 1 && previousPathSubjectPart is IsParentOfPathSubjectPart)
                {
                    result = new GraphPathPart[] { new GraphRootStartNode(name) };
                }
                else
                {
                    result = new GraphPathPart[] { new GraphNode(name) };
                }
                return result;
            });
        }
    }
}