namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    class RootPathProcessor : IRootPathProcessor
    {
        private readonly IProcessingContext _processingContext;
        private readonly IRootHandlerMapperFinder _rootHandlerMapperFinder;
        private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

        public RootPathProcessor(
            IProcessingContext processingContext, 
            IRootHandlerMapperFinder rootHandlerMapperFinder, 
            IRootHandlerPathMatcher rootHandlerPathMatcher)
        {
            _processingContext = processingContext;
            _rootHandlerMapperFinder = rootHandlerMapperFinder;
            _rootHandlerPathMatcher = rootHandlerPathMatcher;
        }

        public void Process(string root, PathSubjectPart[] path, ExecutionScope scope, IObserver<object> output, IScriptScope scriptScope)
        {
            // Find root handler mapper.
            var rootHandlerMapper = _rootHandlerMapperFinder.Find(root);
            if (rootHandlerMapper == null)
            {
                throw new InvalidOperationException("No matching root handler mapper found.");
            }
            // Find the matching root handler.
            //var scriptScope = new ScriptScope()
            var match = rootHandlerMapper.AllowedRootHandlers
                .Select(rh => _rootHandlerPathMatcher.Match(scriptScope, rh, path))
                .FirstOrDefault(m => m != MatchResult.NoMatch);
            if (match == null)
            {
                throw new InvalidOperationException("No matching root handler found.");
            }

            // And process...
            var rootHandler = match.RootHandler;
            rootHandler.Process(_processingContext, match.Match, match.Rest, scope, output);
        }
    }
}