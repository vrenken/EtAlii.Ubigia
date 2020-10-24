namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Linq;
    using System.Threading.Tasks;

    class AllDowndatesRootHandlerPathPartMatcher : IAllDowndatesRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            // Uncertain if this implementation is correct. It could be copied from one of the downdate/update matchers.
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public Task<bool> CanMatch(MatchParameters parameters)
        {
            var canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next is AllDowndatesPathSubjectPart)
            {
                canMatch = true;
            }
            return Task.FromResult(canMatch);
        }
    }
}