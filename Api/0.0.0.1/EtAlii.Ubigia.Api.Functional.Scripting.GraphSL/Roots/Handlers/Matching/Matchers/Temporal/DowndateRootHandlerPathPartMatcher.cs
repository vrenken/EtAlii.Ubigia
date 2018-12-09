namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;

    class DowndateRootHandlerPathPartMatcher : IDowndateRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public bool CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next is DowndatePathSubjectPart)
            {
                canMatch = true;
            }
            return canMatch;
        }
    }
}