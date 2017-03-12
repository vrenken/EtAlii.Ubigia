namespace EtAlii.Ubigia.Api.Functional
{
    public interface IPathVariableExpander
    {
        PathSubjectPart[] Expand(PathSubjectPart[] path);
    }
}