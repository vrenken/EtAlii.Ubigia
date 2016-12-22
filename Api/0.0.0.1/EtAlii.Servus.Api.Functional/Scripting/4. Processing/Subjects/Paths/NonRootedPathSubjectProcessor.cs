namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class NonRootedPathSubjectProcessor : INonRootedPathSubjectProcessor
    {
        private readonly IPathSubjectForOutputConverter _converter;

        public NonRootedPathSubjectProcessor(
            IPathSubjectForOutputConverter converter)
        {
            _converter = converter;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var pathSubject = (PathSubject) subject;
            if (pathSubject is RelativePathSubject)
            {
                // We pass through a relative path.
                output.OnNext(pathSubject);
                output.OnCompleted();
            }
            else
            {
                // And convert absolute and rooted paths.
                _converter.Convert(pathSubject, scope, output);
            }
        }
    }
}
