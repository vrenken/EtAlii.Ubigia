namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class AllChildrenRootHandlerPathPartMatcher : IAllChildrenRootHandlerPathPartMatcher
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
            if (next is AllChildrenPathSubjectPart)
            {
                canMatch = true;
            }
            return Task.FromResult(canMatch);
        }

    }
}
