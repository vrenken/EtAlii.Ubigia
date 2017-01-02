namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByConstantBasedNow2SubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByConstantBasedNow2SubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new ConstantPathSubjectPart("NOW"),
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {

        }
    }
}