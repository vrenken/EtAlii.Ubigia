namespace EtAlii.Servus.Api.Functional
{
    internal interface IRootHandlerPathPartMatcher
    {
        MatchResult[] Match(MatchParameters parameters);
        bool CanMatch(MatchParameters parameters);
    }
}