namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByConstantBasedNow1Handler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;

        public TimeRootByConstantBasedNow1Handler()
        {
            _template = new PathSubjectPart[] {
                    new ConstantPathSubjectPart("now"),
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}