namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class RootHandlerPathMatcher : IRootHandlerPathMatcher
    {
        private readonly IRootHandlerPathPartMatcherSelector _rootHandlerPathPartMatcherSelector;

        public RootHandlerPathMatcher(IRootHandlerPathPartMatcherSelector rootHandlerPathPartMatcherSelector)
        {
            _rootHandlerPathPartMatcherSelector = rootHandlerPathPartMatcherSelector;
        }

        public async Task<MatchResult> Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path)
        {
            var result = new List<PathSubjectPart>();
            var matches = new[] { new MatchResult(rootHandler, new PathSubjectPart[0], path) };
            var templateParts = new Queue<PathSubjectPart>(rootHandler.Template);
            var rest = new PathSubjectPart[0];
            var isFirst = true;

            while (matches.Any())
            {
                if (templateParts.Count == 0)
                {
                    // 0. Break the loop if we do not have any template parts left to search for.
                    var lastMatch = matches.FirstOrDefault();
                    if (lastMatch != null)
                    {
                        result.AddRange(lastMatch.Match);
                        rest = lastMatch.Rest;
                    }
                    break;
                }

                // 1. Get matcher for current template part.
                var templatePart = templateParts.Dequeue();
                var matcher = _rootHandlerPathPartMatcherSelector.Select(templatePart);

                // 2. Find first matching rest.
                rest = await FindMatchingRest(scope, rootHandler, matches, templatePart, matcher, result, isFirst);
                isFirst = false;

                // 3. Get all matches + rests
                var parameters = new MatchParameters(rootHandler, templatePart, rest, scope);
                if (await matcher.CanMatch(parameters))
                {
                    matches = matcher.Match(parameters);
                }
                else
                {
                    // We can't match so lets break and ensure that the algorithm returns MatchResult.NoMatch.
                    result.Clear();
                    rest = new PathSubjectPart[0];
                    break;
                }

                // 4. Sort by length.
                matches = matches
                    .OrderBy(m => m.Rest.Length)
                    .ToArray();
            }

            return templateParts.Count == 0 && (result.Count > 0 || isFirst)
                ? new MatchResult(rootHandler, result.ToArray(), rest) // 5. if we do have matches: Add match to result.
                : MatchResult.NoMatch; // 6. If we do not have matches: fail.
        }

        private async Task<PathSubjectPart[]> FindMatchingRest(
            IScriptScope scope, 
            IRootHandler rootHandler, 
            MatchResult[] matches,
            PathSubjectPart templatePart, 
            IRootHandlerPathPartMatcher matcher, 
            List<PathSubjectPart> result, bool isFirst)
        {
            MatchResult match;
            if (isFirst)
            {
                match = matches[0];
            }
            else
            {
                match = MatchResult.NoMatch;
                foreach (var m in matches)
                {
                    var parameters = new MatchParameters(rootHandler, templatePart, m.Rest, scope);
                    var canMatch = await matcher.CanMatch(parameters);
                    if (canMatch)
                    {
                        match = m;
                        break;
                    }
                }
                result.AddRange(match.Match);
            }

            var rest = match.Rest;
            return rest;
        }
    }
}