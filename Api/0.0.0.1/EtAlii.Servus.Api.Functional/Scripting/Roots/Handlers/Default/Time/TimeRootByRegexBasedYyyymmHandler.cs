namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByRegexBasedYyyymmHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;

        public TimeRootByRegexBasedYyyymmHandler()
        {
            _template = new PathSubjectPart[] {
                    //new RegexPathSubjectPart("STUFF")
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}