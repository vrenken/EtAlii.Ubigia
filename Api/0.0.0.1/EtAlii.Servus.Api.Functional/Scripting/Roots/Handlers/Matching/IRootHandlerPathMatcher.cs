namespace EtAlii.Servus.Api.Functional
{
    public interface IRootHandlerPathMatcher
    {
        MatchResult Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path);
    }
}