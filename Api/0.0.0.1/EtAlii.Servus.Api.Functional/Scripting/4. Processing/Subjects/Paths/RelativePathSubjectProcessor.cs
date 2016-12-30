namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Diagnostics;

    internal class RelativePathSubjectProcessor : IRelativePathSubjectProcessor
    {
        private readonly IPathSubjectForOutputConverter _converter;

        public RelativePathSubjectProcessor(IPathSubjectForOutputConverter converter)
        {
            _converter = converter;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var relativePathSubject = (RelativePathSubject) subject;

            // We pass through a relative path.
            output.OnNext(relativePathSubject);
            output.OnCompleted();
        }
    }
}
