namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Threading.Tasks;

    class UpdatesRootHandlerPathPartMatcher : IUpdatesRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public async Task<bool> CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next is UpdatesPathSubjectPart)
            {
                canMatch = true;
            }
            return await Task.FromResult(canMatch);
        }
    }
}