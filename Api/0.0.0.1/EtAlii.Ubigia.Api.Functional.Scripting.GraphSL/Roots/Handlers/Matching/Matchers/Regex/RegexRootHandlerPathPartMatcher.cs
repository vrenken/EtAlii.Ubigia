namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class RegexRootHandlerPathPartMatcher : IRegexRootHandlerPathPartMatcher
    {
        private readonly IPathSubjectPartContentGetter _pathSubjectPartContentGetter;

        public RegexRootHandlerPathPartMatcher(IPathSubjectPartContentGetter pathSubjectPartContentGetter)
        {
            _pathSubjectPartContentGetter = pathSubjectPartContentGetter;
        }

        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new[] {new MatchResult(null, match, rest)};
        }

        public async Task<bool> CanMatch(MatchParameters parameters)
        {
            var next = parameters.PathRest.FirstOrDefault();
            if (next == null) return false;
            var content = await _pathSubjectPartContentGetter.GetPartContent(next, parameters.Scope);
            if (content == null) return false;
            var regexTemplatePart = (RegexPathSubjectPart) parameters.CurrentTemplatePart;

            var match = Regex.Match(content, regexTemplatePart.Regex, RegexOptions.None);
            var canMatch = match != System.Text.RegularExpressions.Match.Empty;
            return canMatch;
        }
    }
}