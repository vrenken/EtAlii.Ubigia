namespace EtAlii.Servus.Api.Functional
{
    using System.Collections.Generic;

    public interface IRootHandlerPathPartMatcher
    {
        MatchResult[] Match(MatchParameters parameters);
        bool CanMatch(MatchParameters parameters);
    }
}