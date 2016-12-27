namespace EtAlii.Servus.Api.Functional
{
    internal interface IRootHandlerPathMatcher
    {
        MatchResult Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path);
    }
}