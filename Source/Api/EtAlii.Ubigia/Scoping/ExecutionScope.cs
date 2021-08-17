// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The ExecutionScope contains all details needed to execute GCL/GTL/Linq queries and mutations.
    /// </summary>
    public class ExecutionScope //: IExecutionScope
//     {
//         /// <inheritdoc />
//         public Dictionary<string, ScopeVariable> Variables { get; }
//
//         /// <summary>
//         /// Create a new ExecutionScope instance.
//         /// </summary>
//         public ExecutionScope()
//         {
//             Variables = new Dictionary<string, ScopeVariable>();
//         }
//
//         /// <summary>
//         /// Create a new ExecutionScope instance using the variables provided by the dictionary.
//         /// </summary>
//         public ExecutionScope(Dictionary<string, ScopeVariable> variables)
//         {
//             Variables = variables;
//         }
//     }
// }

    {
        /// <summary>
        /// This is the Cache instance used to reduce unnecessary server calls. As the whole entity and relation
        /// model is based on immutability local caching can do tremendous wonders.
        /// </summary>
        public ClientCache Cache { get; }

        private readonly Dictionary<string, Regex> _regexes;

        public Dictionary<string, ScopeVariable> Variables { get; } = new ();

        /// <summary>
        /// Create a new ExecutionScope instance. Set the cacheEnabled to false if the cache should be disabled.
        /// </summary>
        /// <param name="cacheEnabled">False when caching should be disabled.</param>
        public ExecutionScope(bool cacheEnabled = true)
        {
            cacheEnabled = false;
            Cache = new ClientCache(cacheEnabled);
            _regexes = new Dictionary<string, Regex>();
        }

        public ExecutionScope(bool cacheEnabled, Dictionary<string, ScopeVariable> variables)
            : this(cacheEnabled)
        {
            Variables = variables;
        }

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
