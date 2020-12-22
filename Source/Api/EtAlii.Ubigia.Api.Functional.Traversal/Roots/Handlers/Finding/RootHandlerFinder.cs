namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class RootHandlerFinder : IRootHandlerFinder
    {
        private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

        public RootHandlerFinder(IRootHandlerPathMatcher rootHandlerPathMatcher)
        {
            _rootHandlerPathMatcher = rootHandlerPathMatcher;
        }

        public async Task<IRootHandler> Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject)
        {
            var rootHandler = rootHandlerMapper.AllowedRootHandlers.FirstOrDefault();
            if (rootHandler != null)
            {
                var match = await _rootHandlerPathMatcher
                    .Match(scope, rootHandler, rootedPathSubject.Parts)
                    .ConfigureAwait(false);
                return match?.RootHandler;
            }
            return null;
        }
    }
}
