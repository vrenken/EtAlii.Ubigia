namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    public interface IPathSubjectPartContentGetter
    {
        Task<string> GetPartContent(PathSubjectPart part, IScriptScope scope);
    }
}
