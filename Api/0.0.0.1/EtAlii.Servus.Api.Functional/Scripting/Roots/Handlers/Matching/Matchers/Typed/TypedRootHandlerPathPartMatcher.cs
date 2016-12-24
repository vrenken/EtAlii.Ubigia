namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;
    using System.Text.RegularExpressions;

    class TypedRootHandlerPathPartMatcher : ITypedRootHandlerPathPartMatcher
    {
        private readonly IPathSubjectPartContentGetter _pathSubjectPartContentGetter;

        public TypedRootHandlerPathPartMatcher(IPathSubjectPartContentGetter pathSubjectPartContentGetter)
        {
            _pathSubjectPartContentGetter = pathSubjectPartContentGetter;
        }

        public MatchResult[] Match(MatchParameters parameters)
        {
            var match = parameters.PathRest.Take(1).ToArray();
            var rest = parameters.PathRest.Skip(1).ToArray();
            return new MatchResult[] {new MatchResult(null, match, rest)};
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
                    var typedTemplatePart = (TypedPathSubjectPart) parameters.CurrentTemplatePart;

                    var match = Regex.Match(content, typedTemplatePart.Formatter.Regex, RegexOptions.None);
                    canMatch = match != System.Text.RegularExpressions.Match.Empty;
                }
            }
            return canMatch;
        }
    }
}