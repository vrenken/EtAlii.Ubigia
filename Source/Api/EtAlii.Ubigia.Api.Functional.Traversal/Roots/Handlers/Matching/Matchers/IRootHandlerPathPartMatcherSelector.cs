namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IRootHandlerPathPartMatcherSelector
    {
        IRootHandlerPathPartMatcher Select(PathSubjectPart criteria);
    }
}
