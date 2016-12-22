namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    public interface IRootHandlerPathPartMatcherSelector
    {
        IRootHandlerPathPartMatcher Select(PathSubjectPart criteria);
    }
}