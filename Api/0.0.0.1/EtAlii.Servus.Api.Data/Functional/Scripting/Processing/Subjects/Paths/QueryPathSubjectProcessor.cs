namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class QueryPathSubjectProcessor : ISubjectProcessor
    {
        private readonly PathSubjectPartProcessor _pathSubjectPartProcessor;

        public QueryPathSubjectProcessor(
            PathSubjectPartProcessor pathSubjectPartProcessor)
        {
            _pathSubjectPartProcessor = pathSubjectPartProcessor;
        }

        public object Process(ProcessParameters<Subject, SequencePart> parameters)
        {
            object result = null;

            var path = (PathSubject)parameters.Target;
            var parts = path.Parts;
            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                var leftPart = i > 0 ? parts[i - 1] : null;
                var rightPart = i < parts.Length - 1 ? parts[i + 1] : null;
                var pathParameters = new ProcessParameters<PathSubjectPart, PathSubjectPart>(part)
                {
                    LeftPart = leftPart,
                    RightPart = rightPart,
                    LeftResult = result,
                };
                result = _pathSubjectPartProcessor.Process(pathParameters);
                if (result == null)
                {
                    var message = String.Format("Unable to process query path '{0}' (part: {1}, left: {2}, right: {3})", path.ToString(), part, leftPart, rightPart);
                    throw new ScriptProcessingException(message);
                }
            }
            return result;
        }
    }
}
