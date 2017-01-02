namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByPathBasedYYYYSubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByPathBasedYYYYSubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimeRootHandler.YearFormatter)
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}