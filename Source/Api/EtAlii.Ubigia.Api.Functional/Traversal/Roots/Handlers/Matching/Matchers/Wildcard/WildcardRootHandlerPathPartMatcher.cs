namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class WildcardRootHandlerPathPartMatcher : IWildcardRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public Task<bool> CanMatch(MatchParameters parameters)
        {
            var canMatch = false;

            var next = parameters.PathRest.FirstOrDefault();
            if (next != null)
            {
                var pathPattern = (next as WildcardPathSubjectPart)?.Pattern;
                //var templatePattern = ((WildcardPathSubjectPart) parameters.CurrentTemplatePart).Pattern

                //TODO: Currently a wildcard path always matches. We might want to change this in the future.
                canMatch = pathPattern != null;
            }
            return Task.FromResult(canMatch);
        }

    }
}
