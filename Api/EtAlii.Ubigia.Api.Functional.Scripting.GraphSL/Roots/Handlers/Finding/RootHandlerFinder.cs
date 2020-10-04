namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Linq;
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
            var rootHandler = rootHandlerMapper.AllowedRootHandlers.FirstOrDefault();
            return rootHandler != null 
                ? (await _rootHandlerPathMatcher.Match(scope, rootHandler, rootedPathSubject.Parts))?.RootHandler
                : null;
        }
    }
}