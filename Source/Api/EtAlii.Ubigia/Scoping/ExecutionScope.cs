namespace EtAlii.Ubigia
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    // TODO: Move to Traversal project in main repository.
    /// <summary>
    /// The ExecutionScope contains all details needed to execute GXL/GTL/Linq queries and mutations.
    /// </summary>
    public class ExecutionScope
    {
        /// <summary>
        /// This is the Cache instance used to reduce unnecessary server calls. As the whole entity and relation
        /// model is based on immutability local caching can do tremendous wonders.
        /// </summary>
        public Cache Cache { get; }

        private readonly Dictionary<string, Regex> _regexes;

        /// <summary>
        /// Create a new ExecutionScope instance. Set the cacheEnabled to false if the cache should be disabled.
        /// </summary>
        /// <param name="cacheEnabled">False when caching should be disabled.</param>
        public ExecutionScope(bool cacheEnabled)
        {
            Cache = new Cache(cacheEnabled);
            _regexes = new Dictionary<string, Regex>();
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
