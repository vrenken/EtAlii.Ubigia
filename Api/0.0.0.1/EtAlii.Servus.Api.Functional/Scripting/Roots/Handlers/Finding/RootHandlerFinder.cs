namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;

    class RootHandlerFinder : IRootHandlerFinder
    {
        private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

        public RootHandlerFinder(IRootHandlerPathMatcher rootHandlerPathMatcher)
        {
            _rootHandlerPathMatcher = rootHandlerPathMatcher;
        }

        public IRootHandler Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject)
        {
            return rootHandlerMapper.AllowedPaths
                .Select(rootHandler => _rootHandlerPathMatcher.Match(scope, rootHandler, rootedPathSubject.Parts))
                .Select(match => match.RootHandler)
                .FirstOrDefault();
        }
    }
}