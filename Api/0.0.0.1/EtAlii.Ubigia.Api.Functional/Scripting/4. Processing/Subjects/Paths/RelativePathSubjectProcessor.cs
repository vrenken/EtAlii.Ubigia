namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class RelativePathSubjectProcessor : IRelativePathSubjectProcessor
    {
        private readonly IRootPathProcessor _rootPathProcessor;
        private readonly IPathVariableExpander _pathVariableExpander;
        private readonly IPathSubjectForOutputConverter _converter;
        private readonly IPathSubjectPartContentGetter _partContentGetter;
        private readonly IProcessingContext _processingContext;

        public RelativePathSubjectProcessor(
            IRootPathProcessor rootPathProcessor, 
            IPathVariableExpander pathVariableExpander, 
            IPathSubjectForOutputConverter converter, 
            IPathSubjectPartContentGetter partContentGetter, 
            IProcessingContext processingContext)
        {
            _rootPathProcessor = rootPathProcessor;
            _pathVariableExpander = pathVariableExpander;
            _converter = converter;
            _partContentGetter = partContentGetter;
            _processingContext = processingContext;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var pathSubject = (RelativePathSubject) subject;

            // Let's expand all possible variables within the path.
            var parts = _pathVariableExpander.Expand(pathSubject.Parts);

            if (parts[1] is IdentifierPathSubjectPart)
            {
                // Straight conversion of all paths that start with an identifier.
                _converter.Convert(pathSubject, scope, output);

            }
            else if (parts[0] is IsParentOfPathSubjectPart)
            {
                // Ok, we can translate the path into a rooted path. let's do so.
                var root = _partContentGetter.GetPartContent(parts.Skip(1).First(), _processingContext.Scope); 
                var path = parts.Length > 3
                    ? parts.Skip(3).ToArray()
                    : new PathSubjectPart[0];
                _rootPathProcessor.Process(root, path, scope, output, _processingContext.Scope);

            }
            else
            {
                // We pass through a relative path.
                output.OnNext(pathSubject);
                output.OnCompleted();
            }
        }
    }
}
