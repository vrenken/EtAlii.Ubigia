namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    internal class RootedPathSubjectProcessor : IRootedPathSubjectProcessor
    {
        private readonly IRootContext _rootContext;
        private readonly IRootHandlerMapperFinder _rootHandlerMapperFinder;
        private readonly IRootHandlerFinder _rootHandlerFinder;
        private readonly IPathSubjectForOutputConverter _converter;
        private readonly IRootHandlerPathMatcher _rootHandlerPathMatcher;

        public RootedPathSubjectProcessor(
            IRootContext rootContext,
            IRootHandlerMapperFinder rootHandlerMapperFinder, 
            IRootHandlerFinder rootHandlerFinder, 
            IPathSubjectForOutputConverter converter, 
            IRootHandlerPathMatcher rootHandlerPathMatcher)
        {
            _rootContext = rootContext;
            _rootHandlerMapperFinder = rootHandlerMapperFinder;
            _rootHandlerFinder = rootHandlerFinder;
            _converter = converter;
            _rootHandlerPathMatcher = rootHandlerPathMatcher;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            Debug.Assert(subject is RootedPathSubject, $"The {nameof(RootedPathSubjectProcessor)} can only process {nameof(RootedPathSubject)}s");
           
            var rootedPathSubject = (RootedPathSubject) subject;

            // Find root handler mapper.
            var rootHandlerMapper = _rootHandlerMapperFinder.Find(rootedPathSubject);
            if (rootHandlerMapper == null)
            {
                throw new InvalidOperationException("No matching root handler mapper found.");
            }
            // Find the matching root handler.
            var scriptScope = new ScriptScope();
            var match = rootHandlerMapper.AllowedPaths
                .Select(rh => _rootHandlerPathMatcher.Match(scriptScope, rh, rootedPathSubject.Parts))
                .FirstOrDefault(m => m != MatchResult.NoMatch);
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
