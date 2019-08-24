namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    public interface IPathVariableExpander
    {
        Task<PathSubjectPart[]> Expand(PathSubjectPart[] path);
    }
}