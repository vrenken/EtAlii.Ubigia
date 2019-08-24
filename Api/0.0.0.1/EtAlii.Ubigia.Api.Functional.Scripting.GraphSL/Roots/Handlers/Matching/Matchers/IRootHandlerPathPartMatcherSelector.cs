namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IRootHandlerPathPartMatcherSelector
    {
        IRootHandlerPathPartMatcher Select(PathSubjectPart criteria);
    }
}