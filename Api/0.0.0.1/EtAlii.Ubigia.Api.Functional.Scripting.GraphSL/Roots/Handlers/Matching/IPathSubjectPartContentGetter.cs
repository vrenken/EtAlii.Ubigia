namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    public interface IPathSubjectPartContentGetter
    {
        Task<string> GetPartContent(PathSubjectPart part, IScriptScope scope);
    }
}