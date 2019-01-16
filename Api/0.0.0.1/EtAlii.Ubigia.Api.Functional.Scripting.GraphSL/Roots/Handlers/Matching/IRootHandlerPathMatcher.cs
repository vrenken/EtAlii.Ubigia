namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IRootHandlerPathMatcher
    {
        MatchResult Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path);
    }
}