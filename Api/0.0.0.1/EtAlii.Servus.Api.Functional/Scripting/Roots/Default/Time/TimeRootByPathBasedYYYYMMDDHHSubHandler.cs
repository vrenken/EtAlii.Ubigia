namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByPathBasedYYYYMMDDHHSubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByPathBasedYYYYMMDDHHSubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimeRootHandler.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.MonthFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.DayFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.HourFormatter)
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}