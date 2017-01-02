namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByConstantBasedNow3SubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByConstantBasedNow3SubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new ConstantPathSubjectPart("Now"),
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {

        }
    }
}