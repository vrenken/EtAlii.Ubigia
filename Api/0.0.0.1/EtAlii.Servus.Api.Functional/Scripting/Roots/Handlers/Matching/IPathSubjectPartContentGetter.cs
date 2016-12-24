namespace EtAlii.Servus.Api.Functional
{
    public interface IPathSubjectPartContentGetter
    {
        string GetPartContent(PathSubjectPart part, IScriptScope scope);
    }
}