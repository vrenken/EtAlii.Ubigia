namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByPathBasedYYYYMMDDSubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByPathBasedYYYYMMDDSubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimeRootHandler.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.MonthFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TimeRootHandler.DayFormatter)
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}