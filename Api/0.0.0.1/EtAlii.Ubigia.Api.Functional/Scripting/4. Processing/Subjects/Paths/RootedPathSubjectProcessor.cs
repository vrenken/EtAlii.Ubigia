namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class RootedPathSubjectProcessor : IRootedPathSubjectProcessor
    {
        private readonly IRootPathProcessor _rootPathProcessor;
        private readonly IPathVariableExpander _pathVariableExpander;
        private readonly IProcessingContext _processingContext;

        public RootedPathSubjectProcessor(
            IRootPathProcessor rootPathProcessor, 
            IPathVariableExpander pathVariableExpander, 
            IProcessingContext processingContext)
        {
            _rootPathProcessor = rootPathProcessor;
            _pathVariableExpander = pathVariableExpander;
            _processingContext = processingContext;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var pathSubject = (RootedPathSubject) subject;

            // Let's expand all possible variables within the path.
            var parts = _pathVariableExpander.Expand(pathSubject.Parts);

            // And handover the root and following path for root path processing.
            _rootPathProcessor.Process(pathSubject.Root, parts, scope, output, _processingContext.Scope);
        }
    }
}
