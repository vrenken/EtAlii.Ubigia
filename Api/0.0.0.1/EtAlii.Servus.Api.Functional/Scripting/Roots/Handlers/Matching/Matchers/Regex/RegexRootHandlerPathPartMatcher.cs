namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class RegexRootHandlerPathPartMatcher : IRegexRootHandlerPathPartMatcher
    {
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
                var content = GetPartContent(next);
                var regexTemplatePart = (RegexPathSubjectPart)parameters.CurrentTemplatePart;

                var match = Regex.Match(content, regexTemplatePart.Regex, RegexOptions.None);
                canMatch = match != System.Text.RegularExpressions.Match.Empty;
            }
            return canMatch;
        }

        private string GetPartContent(PathSubjectPart part)
        {
            var constantPathSubjectPart = part as ConstantPathSubjectPart;
            if (constantPathSubjectPart != null)
            {
                return constantPathSubjectPart.Name;
            }
            else
            {
                throw new InvalidOperationException("Unable to get content for part: " + part);
            }
        }
    }
}