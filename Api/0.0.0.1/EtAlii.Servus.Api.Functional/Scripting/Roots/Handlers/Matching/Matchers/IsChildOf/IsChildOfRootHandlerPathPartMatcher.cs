namespace EtAlii.Servus.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;

    class IsChildOfRootHandlerPathPartMatcher : IIsChildOfRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new MatchResult[] { new MatchResult(null, match, rest) };
        }

        public bool CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next is IsChildOfPathSubjectPart)
            {
                canMatch = true;
            }
            return canMatch;
        }

    }
}