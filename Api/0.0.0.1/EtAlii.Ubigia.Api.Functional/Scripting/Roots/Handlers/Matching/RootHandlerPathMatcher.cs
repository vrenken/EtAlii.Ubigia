namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Structure;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class RootHandlerPathMatcher : IRootHandlerPathMatcher
    {
        private readonly IRootHandlerPathPartMatcherSelector _rootHandlerPathPartMatcherSelector;

        public RootHandlerPathMatcher(IRootHandlerPathPartMatcherSelector rootHandlerPathPartMatcherSelector)
        {
            _rootHandlerPathPartMatcherSelector = rootHandlerPathPartMatcherSelector;
        }

        public MatchResult Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path)
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
                MatchResult match;
                if (isFirst)
                {
                    match = matches[0];
                    isFirst = false;
                }
                else
                {
                    match = FindFirstMatchingResult(rootHandler, matches, templatePart, matcher, scope);
                    result.AddRange(match.Match);
                }
                rest = match.Rest;

                // 3. Get all matches + rests
                var parameters = new MatchParameters(rootHandler, templatePart, rest, scope);
                if (matcher.CanMatch(parameters))
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

            if (templateParts.Count == 0 && (result.Count > 0 || isFirst))
            {
                // 5. if we do have matches: Add match to result.
                return new MatchResult(rootHandler, result.ToArray(), rest);
            }
            else
            {
                // 6. If we do not have matches: fail.
                return MatchResult.NoMatch;
            }
        }

        private MatchResult FindFirstMatchingResult(
            IRootHandler rootHandler,
            MatchResult[] matches, 
            PathSubjectPart templatePart, 
            IRootHandlerPathPartMatcher matcher, 
            IScriptScope scope)
        {
            return matches
                .Where(m =>
                {
                    var parameters = new MatchParameters(rootHandler, templatePart, m.Rest, scope);
                    var canMatch = matcher.CanMatch(parameters);
                    return canMatch;
                })
                .FirstOrDefault() ?? MatchResult.NoMatch;
        }
    }
}