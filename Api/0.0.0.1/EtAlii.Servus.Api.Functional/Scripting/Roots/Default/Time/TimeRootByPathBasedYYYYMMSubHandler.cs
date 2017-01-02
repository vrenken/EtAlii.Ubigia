namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByPathBasedYYYYMMSubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByPathBasedYYYYMMSubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimeRootHandler.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.MonthFormatter)
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}