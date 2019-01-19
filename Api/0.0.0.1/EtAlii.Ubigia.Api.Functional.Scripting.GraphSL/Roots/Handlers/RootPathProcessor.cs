namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    class RootPathProcessor : IRootPathProcessor
    {
        private readonly IRootContext _rootContext;
        private readonly IRootHandlerMapperFinder _rootHandlerMapperFinder;
        private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

        public RootPathProcessor(
            IRootContext rootContext, 
            IRootHandlerMapperFinder rootHandlerMapperFinder, 
            IRootHandlerPathMatcher rootHandlerPathMatcher)
        {
            _rootContext = rootContext;
            _rootHandlerMapperFinder = rootHandlerMapperFinder;
            _rootHandlerPathMatcher = rootHandlerPathMatcher;
        }

        public async Task Process(string root, PathSubjectPart[] path, ExecutionScope scope, IObserver<object> output, IScriptScope scriptScope)
        {
            // Find root handler mapper.
            var rootHandlerMapper = _rootHandlerMapperFinder.Find(root);
            if (rootHandlerMapper == null)
            {
                throw new InvalidOperationException("No matching root handler mapper found.");
            }
            // Find the matching root handler.
            //var scriptScope = new ScriptScope();

            MatchResult match = null;

            foreach (var rh in rootHandlerMapper.AllowedRootHandlers)
            {
                var result = await _rootHandlerPathMatcher.Match(scriptScope, rh, path);
                if (result != MatchResult.NoMatch)
                {
                    match = result;
                    break;
                }
            }
            if (match == null)
            {
                throw new InvalidOperationException("No matching root handler found.");
            }

            // And process...
            var rootHandler = match.RootHandler;
            rootHandler.Process(_rootContext, match.Match, match.Rest, scope, output);
        }
    }
}