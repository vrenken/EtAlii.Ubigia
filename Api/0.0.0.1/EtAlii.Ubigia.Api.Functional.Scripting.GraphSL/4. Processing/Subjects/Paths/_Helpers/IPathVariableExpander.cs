namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    public interface IPathVariableExpander
    {
        Task<PathSubjectPart[]> Expand(PathSubjectPart[] path);
    }
}