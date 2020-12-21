namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRootHandlerPathPartMatcher
    {
        MatchResult[] Match(MatchParameters parameters);
        Task<bool> CanMatch(MatchParameters parameters);
    }
}
