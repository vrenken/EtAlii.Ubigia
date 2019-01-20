namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    class RootHandlerFinder : IRootHandlerFinder
    {
        private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

        public RootHandlerFinder(IRootHandlerPathMatcher rootHandlerPathMatcher)
        {
            _rootHandlerPathMatcher = rootHandlerPathMatcher;
        }

        public async Task<IRootHandler> Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject)
        {
            foreach (var rootHandler in rootHandlerMapper.AllowedRootHandlers)
            {
                var match = await _rootHandlerPathMatcher.Match(scope, rootHandler, rootedPathSubject.Parts);
                if (match != MatchResult.NoMatch)
                {
                    return match.RootHandler;
                }
            }

            return null;
        }
    }
}