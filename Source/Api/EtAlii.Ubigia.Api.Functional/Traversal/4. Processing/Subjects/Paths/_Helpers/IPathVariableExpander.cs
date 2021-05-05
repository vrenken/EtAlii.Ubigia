namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    public interface IPathVariableExpander
    {
        Task<PathSubjectPart[]> Expand(PathSubjectPart[] path);
    }
}
