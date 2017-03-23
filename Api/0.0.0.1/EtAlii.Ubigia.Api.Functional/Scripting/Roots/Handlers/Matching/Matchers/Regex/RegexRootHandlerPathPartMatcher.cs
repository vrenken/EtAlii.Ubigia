namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Text.RegularExpressions;

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

        public bool CanMatch(MatchParameters parameters)
        {
            bool canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next != null)
            {
                var content = _pathSubjectPartContentGetter.GetPartContent(next, parameters.Scope);
                if (content != null)
                {
                    var regexTemplatePart = (RegexPathSubjectPart) parameters.CurrentTemplatePart;

                    var match = Regex.Match(content, regexTemplatePart.Regex, RegexOptions.None);
                    canMatch = match != System.Text.RegularExpressions.Match.Empty;
                }
            }
            return canMatch;
        }
    }
}