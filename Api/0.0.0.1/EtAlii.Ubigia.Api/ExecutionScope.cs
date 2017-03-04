namespace EtAlii.Ubigia.Api
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ExecutionScope
    {
        public Cache Cache => _cache;
        private readonly Cache _cache;

        private readonly Dictionary<string, Regex> _regexes;

        public ExecutionScope(bool cacheEnabled)
        {
            _cache = new Cache(cacheEnabled);
            _regexes = new Dictionary<string, Regex>();
        }


        public Regex GetWildCardRegex(string pattern)
        {
            Regex result = null;
            if (!_regexes.TryGetValue(pattern, out result))
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