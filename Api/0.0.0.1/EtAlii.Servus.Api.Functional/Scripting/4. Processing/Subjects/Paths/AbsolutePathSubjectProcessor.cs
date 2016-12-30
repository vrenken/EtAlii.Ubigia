namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class AbsolutePathSubjectProcessor : IAbsolutePathSubjectProcessor
    {
        private readonly IPathSubjectForOutputConverter _converter;

        public AbsolutePathSubjectProcessor(IPathSubjectForOutputConverter converter)
        {
            _converter = converter;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var absolutePathSubject = (AbsolutePathSubject) subject;

            // And convert absolute paths.
            _converter.Convert(absolutePathSubject, scope, output);
        }
    }
}
