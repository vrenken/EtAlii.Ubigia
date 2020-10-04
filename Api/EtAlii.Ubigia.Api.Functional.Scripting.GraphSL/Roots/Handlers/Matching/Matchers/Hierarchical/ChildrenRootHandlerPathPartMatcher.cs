namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Linq;
    using System.Threading.Tasks;

    class ChildrenRootHandlerPathPartMatcher : IChildrenRootHandlerPathPartMatcher
    {
        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] { new MatchResult(null, match, rest) };
        }

        public Task<bool> CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next is ChildrenPathSubjectPart)
            {
                canMatch = true;
            }
            return Task.FromResult(canMatch);
        }

    }
}