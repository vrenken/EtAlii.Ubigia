namespace EtAlii.Servus.Api.Functional
{
    internal interface IRootHandlerPathPartMatcherSelector
    {
        IRootHandlerPathPartMatcher Select(PathSubjectPart criteria);
    }
}