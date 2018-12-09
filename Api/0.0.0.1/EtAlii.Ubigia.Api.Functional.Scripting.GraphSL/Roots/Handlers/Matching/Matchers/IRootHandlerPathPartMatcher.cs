namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IRootHandlerPathPartMatcher
    {
        MatchResult[] Match(MatchParameters parameters);
        bool CanMatch(MatchParameters parameters);
    }
}