namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRootHandlerPathMatcher
    {
        Task<MatchResult> Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path);
    }
}
