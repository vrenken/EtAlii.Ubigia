namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class ConstantPathSubjectPartToGraphPathPartsConverter : IConstantPathSubjectPartToGraphPathPartsConverter
    {
        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            GraphPathPart[] result;
            var name = ((ConstantPathSubjectPart)pathSubjectPart).Name;

            if (pathSubjectPartPosition == 0 ||
                pathSubjectPartPosition == 1 && previousPathSubjectPart is ParentPathSubjectPart)
            {
                result = new GraphPathPart[] { new GraphRootStartNode(name) };
            }
            else
            {
                result = new GraphPathPart[] { new GraphNode(name) };
            }
            return Task.FromResult(result);
        }
    }
}