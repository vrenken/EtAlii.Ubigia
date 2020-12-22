namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal interface IPathSubjectPartToGraphPathPartsConverter
    {
        Task<GraphPathPart[]> Convert(
            PathSubjectPart pathSubjectPart,
            int pathSubjectPartPosition,
            PathSubjectPart previousPathSubjectPart,
            PathSubjectPart nextPathSubjectPart,
            ExecutionScope scope);
    }
}
