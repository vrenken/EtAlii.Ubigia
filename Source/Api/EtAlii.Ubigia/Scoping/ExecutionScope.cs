// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The ExecutionScope contains all details needed to execute GCL/GTL/Linq queries and mutations.
    /// </summary>
    public class ExecutionScope
    {
        /// <summary>
        /// This is the Cache instance used to reduce unnecessary Property server calls. As the whole entity and relation
        /// model is based on immutability local caching can do tremendous wonders.
        /// </summary>
        public IDictionary<Identifier, PropertyDictionary> PropertyCache { get; } = new Dictionary<Identifier, PropertyDictionary>();

        public IDictionary<string, Root> RootCache { get; } = new Dictionary<string, Root>();

        /// <summary>
        /// This is the Cache instance used to reduce unnecessary Entry server calls. As the whole entity and relation
        /// model is based on immutability local caching can do tremendous wonders.
        /// </summary>
        public IDictionary<Identifier, IReadOnlyEntry> EntryCache { get; } = new Dictionary<Identifier, IReadOnlyEntry>();

        /// <summary>
        /// This is the Cache instance used to reduce unnecessary EntryRelations server calls. As the whole entity and relation
        /// model is based on immutability local caching can do tremendous wonders.
        /// </summary>
        public IDictionary<Tuple<Identifier, EntryRelations>, IEnumerable<IReadOnlyEntry>> EntryRelationCache { get; } = new Dictionary<Tuple<Identifier, EntryRelations>, IEnumerable<IReadOnlyEntry>>();

        private readonly Dictionary<string, Regex> _regexes = new ();

        public Dictionary<string, ScopeVariable> Variables { get; } = new ();

        /// <summary>
        /// Gets cached regular expressions for the specified pattern.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Regex GetWildCardRegex(string pattern)
        {
            if (!_regexes.TryGetValue(pattern, out var result))
            {
                var regexPattern = WildCardPatternToRegexPattern(pattern);
                var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                _regexes[pattern] = result = regex;
            }
            return result;
        }

        private string WildCardPatternToRegexPattern(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                           .Replace(@"\*", ".*")
                           .Replace(@"\?", ".")
                       + "$";
        }
    }
}
