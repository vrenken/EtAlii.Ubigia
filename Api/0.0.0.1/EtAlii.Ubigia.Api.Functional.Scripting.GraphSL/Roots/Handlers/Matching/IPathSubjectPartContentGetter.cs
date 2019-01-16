namespace EtAlii.Ubigia.Api.Functional
{
    public interface IPathSubjectPartContentGetter
    {
        string GetPartContent(PathSubjectPart part, IScriptScope scope);
    }
}