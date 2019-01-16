namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;

    class PreviousRootHandlerPathPartMatcher : IPreviousRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            // Uncertain if this implementation is correct. It could be copied from one of the downdate/update matchers.
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public bool CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next is PreviousPathSubjectPart)
            {
                canMatch = true;
            }
            return canMatch;
        }
    }
}