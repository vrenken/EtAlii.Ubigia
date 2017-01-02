namespace EtAlii.Servus.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

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