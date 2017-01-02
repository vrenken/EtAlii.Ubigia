namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByPathBasedYYYYMMDDHHMMSubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByPathBasedYYYYMMDDHHMMSubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimeRootHandler.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.MonthFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.DayFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.HourFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.MinuteFormatter)
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}