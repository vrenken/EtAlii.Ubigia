// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    internal class TypedRootHandlerPathPartMatcher : ITypedRootHandlerPathPartMatcher
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
            return new[] {new MatchResult(null, match, rest)};
        }

        public async Task<bool> CanMatch(MatchParameters parameters)
        {
            var canMatch = false;
            var next = parameters.PathRest.FirstOrDefault();
            if (next != null)
            {
                var content = await _pathSubjectPartContentGetter.GetPartContent(next, parameters.Scope).ConfigureAwait(false);
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
