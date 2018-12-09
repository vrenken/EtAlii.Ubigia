namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IRootHandlerPathPartMatcherSelector
    {
        IRootHandlerPathPartMatcher Select(PathSubjectPart criteria);
    }
}