namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByConstantBasedNow1SubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByConstantBasedNow1SubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new ConstantPathSubjectPart("now"),
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}