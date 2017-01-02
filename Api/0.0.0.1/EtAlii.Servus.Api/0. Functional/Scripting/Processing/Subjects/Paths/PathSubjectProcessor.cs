namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class PathSubjectProcessor : IPathSubjectProcessor
    {
        private readonly IPathSubjectForOutputConverter _converter;

        public PathSubjectProcessor(
            IPathSubjectForOutputConverter converter)
        {
            _converter = converter;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var pathSubject = (PathSubject) subject;
            if (pathSubject.IsAbsolute)
            {
                _converter.Convert(pathSubject, scope, output);
            }
            else
            {
                output.OnNext(pathSubject);
                output.OnCompleted();
            }
        }
    }
}
