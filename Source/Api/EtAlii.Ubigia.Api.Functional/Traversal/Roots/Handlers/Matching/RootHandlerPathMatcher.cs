// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class RootHandlerPathMatcher : IRootHandlerPathMatcher
    {
        private readonly ITypedRootHandlerPathPartMatcher _typedRootHandlerPathPartMatcher;
        private readonly IRegexRootHandlerPathPartMatcher _regexRootHandlerPathPartMatcher;
        private readonly IConstantRootHandlerPathPartMatcher _constantRootHandlerPathPartMatcher;
        private readonly IAllParentsRootHandlerPathPartMatcher _allParentsRootHandlerPathPartMatcher;
        private readonly IParentRootHandlerPathPartMatcher _parentRootHandlerPathPartMatcher;
        private readonly IAllChildrenRootHandlerPathPartMatcher _allChildrenRootHandlerPathPartMatcher;
        private readonly IChildrenRootHandlerPathPartMatcher _childrenRootHandlerPathPartMatcher;
        private readonly IAllNextRootHandlerPathPartMatcher _allNextRootHandlerPathPartMatcher;
        private readonly INextRootHandlerPathPartMatcher _nextRootHandlerPathPartMatcher;
        private readonly IAllPreviousRootHandlerPathPartMatcher _allPreviousRootHandlerPathPartMatcher;
        private readonly IPreviousRootHandlerPathPartMatcher _previousRootHandlerPathPartMatcher;
        private readonly IWildcardRootHandlerPathPartMatcher _wildcardRootHandlerPathPartMatcher;
        private readonly IVariableRootHandlerPathPartMatcher _variableRootHandlerPathPartMatcher;
        private readonly IConditionalRootHandlerPathPartMatcher _conditionalRootHandlerPathPartMatcher;
        private readonly IAllUpdatesRootHandlerPathPartMatcher _allUpdatesRootHandlerPathPartMatcher;
        private readonly IUpdatesRootHandlerPathPartMatcher _updatesRootHandlerPathPartMatcher;
        private readonly IAllDowndatesRootHandlerPathPartMatcher _allDowndatesRootHandlerPathPartMatcher;
        private readonly IDowndateRootHandlerPathPartMatcher _downdateRootHandlerPathPartMatcher;
        private readonly IIdentifierRootHandlerPathPartMatcher _identifierRootHandlerPathPartMatcher;

        public RootHandlerPathMatcher(
            ITypedRootHandlerPathPartMatcher typedRootHandlerPathPartMatcher,
            IRegexRootHandlerPathPartMatcher regexRootHandlerPathPartMatcher,
            IConstantRootHandlerPathPartMatcher constantRootHandlerPathPartMatcher,
            IAllParentsRootHandlerPathPartMatcher allParentsRootHandlerPathPartMatcher,
            IParentRootHandlerPathPartMatcher parentRootHandlerPathPartMatcher,
            IAllChildrenRootHandlerPathPartMatcher allChildrenRootHandlerPathPartMatcher,
            IChildrenRootHandlerPathPartMatcher childrenRootHandlerPathPartMatcher,
            IAllNextRootHandlerPathPartMatcher allNextRootHandlerPathPartMatcher,
            INextRootHandlerPathPartMatcher nextRootHandlerPathPartMatcher,
            IAllPreviousRootHandlerPathPartMatcher allPreviousRootHandlerPathPartMatcher,
            IPreviousRootHandlerPathPartMatcher previousRootHandlerPathPartMatcher,
            IWildcardRootHandlerPathPartMatcher wildcardRootHandlerPathPartMatcher,
            IVariableRootHandlerPathPartMatcher variableRootHandlerPathPartMatcher,
            IConditionalRootHandlerPathPartMatcher conditionalRootHandlerPathPartMatcher,
            IAllUpdatesRootHandlerPathPartMatcher allUpdatesRootHandlerPathPartMatcher,
            IUpdatesRootHandlerPathPartMatcher updatesRootHandlerPathPartMatcher,
            IAllDowndatesRootHandlerPathPartMatcher allDowndatesRootHandlerPathPartMatcher,
            IDowndateRootHandlerPathPartMatcher downdateRootHandlerPathPartMatcher,
            IIdentifierRootHandlerPathPartMatcher identifierRootHandlerPathPartMatcher)
        {
            _typedRootHandlerPathPartMatcher = typedRootHandlerPathPartMatcher;
            _regexRootHandlerPathPartMatcher = regexRootHandlerPathPartMatcher;
            _constantRootHandlerPathPartMatcher = constantRootHandlerPathPartMatcher;
            _allParentsRootHandlerPathPartMatcher = allParentsRootHandlerPathPartMatcher;
            _parentRootHandlerPathPartMatcher = parentRootHandlerPathPartMatcher;
            _allChildrenRootHandlerPathPartMatcher = allChildrenRootHandlerPathPartMatcher;
            _childrenRootHandlerPathPartMatcher = childrenRootHandlerPathPartMatcher;
            _allNextRootHandlerPathPartMatcher = allNextRootHandlerPathPartMatcher;
            _nextRootHandlerPathPartMatcher = nextRootHandlerPathPartMatcher;
            _allPreviousRootHandlerPathPartMatcher = allPreviousRootHandlerPathPartMatcher;
            _previousRootHandlerPathPartMatcher = previousRootHandlerPathPartMatcher;
            _wildcardRootHandlerPathPartMatcher = wildcardRootHandlerPathPartMatcher;
            _variableRootHandlerPathPartMatcher = variableRootHandlerPathPartMatcher;
            _conditionalRootHandlerPathPartMatcher = conditionalRootHandlerPathPartMatcher;
            _allUpdatesRootHandlerPathPartMatcher = allUpdatesRootHandlerPathPartMatcher;
            _updatesRootHandlerPathPartMatcher = updatesRootHandlerPathPartMatcher;
            _allDowndatesRootHandlerPathPartMatcher = allDowndatesRootHandlerPathPartMatcher;
            _downdateRootHandlerPathPartMatcher = downdateRootHandlerPathPartMatcher;
            _identifierRootHandlerPathPartMatcher = identifierRootHandlerPathPartMatcher;
        }
        public async Task<MatchResult> Match(IScriptScope scope, IRootHandler rootHandler, PathSubjectPart[] path)
        {
            var result = new List<PathSubjectPart>();
            var matches = new[] { new MatchResult(rootHandler, Array.Empty<PathSubjectPart>(), path) };
            var templateParts = new Queue<PathSubjectPart>(rootHandler.Template);
            var rest = Array.Empty<PathSubjectPart>();
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

                IRootHandlerPathPartMatcher matcher = templatePart switch
                {
                    TypedPathSubjectPart => _typedRootHandlerPathPartMatcher,
                    RegexPathSubjectPart => _regexRootHandlerPathPartMatcher,
                    ConstantPathSubjectPart => _constantRootHandlerPathPartMatcher,
                    AllParentsPathSubjectPart => _allParentsRootHandlerPathPartMatcher,
                    ParentPathSubjectPart => _parentRootHandlerPathPartMatcher,
                    AllChildrenPathSubjectPart => _allChildrenRootHandlerPathPartMatcher,
                    ChildrenPathSubjectPart => _childrenRootHandlerPathPartMatcher,
                    AllNextPathSubjectPart => _allNextRootHandlerPathPartMatcher,
                    NextPathSubjectPart => _nextRootHandlerPathPartMatcher,
                    AllPreviousPathSubjectPart => _allPreviousRootHandlerPathPartMatcher,
                    PreviousPathSubjectPart => _previousRootHandlerPathPartMatcher,
                    WildcardPathSubjectPart => _wildcardRootHandlerPathPartMatcher,
                    VariablePathSubjectPart => _variableRootHandlerPathPartMatcher,
                    ConditionalPathSubjectPart => _conditionalRootHandlerPathPartMatcher,
                    AllUpdatesPathSubjectPart => _allUpdatesRootHandlerPathPartMatcher,
                    UpdatesPathSubjectPart => _updatesRootHandlerPathPartMatcher,
                    DowndatePathSubjectPart => _downdateRootHandlerPathPartMatcher,
                    AllDowndatesPathSubjectPart => _allDowndatesRootHandlerPathPartMatcher,
                    IdentifierPathSubjectPart => _identifierRootHandlerPathPartMatcher,
                    _ => throw new NotSupportedException($"Cannot find matcher for {templatePart?.ToString() ?? "NULL"}")
                };

                // 2. Find first matching rest.
                rest = await FindMatchingRest(scope, rootHandler, matches, templatePart, matcher, result, isFirst).ConfigureAwait(false);
                isFirst = false;

                // 3. Get all matches + rests
                var parameters = new MatchParameters(rootHandler, templatePart, rest, scope);
                var canMatch = await matcher.CanMatch(parameters).ConfigureAwait(false);
                if (canMatch)
                {
                    matches = matcher.Match(parameters);
                }
                else
                {
                    // We can't match so lets break and ensure that the algorithm returns MatchResult.NoMatch.
                    result.Clear();
                    rest = Array.Empty<PathSubjectPart>();
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
                    var canMatch = await matcher.CanMatch(parameters).ConfigureAwait(false);
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
