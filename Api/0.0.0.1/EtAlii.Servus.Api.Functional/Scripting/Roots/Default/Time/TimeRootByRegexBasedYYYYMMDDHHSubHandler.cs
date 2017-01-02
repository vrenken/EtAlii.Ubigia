namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class TimeRootByRegexBasedYYYYMMDDHHSubHandler : IRootSubHandler
    {

        public PathSubjectPart[] AllowedPaths { get { return _allowedParts; } }
        private readonly PathSubjectPart[] _allowedParts;

        public TimeRootByRegexBasedYYYYMMDDHHSubHandler()
        {
            _allowedParts = new PathSubjectPart[] {
                    //new RegexPathSubjectPart("STUFF")
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope,
            IObserver<object> output, bool processAsSubject)
        {
            
        }
    }
}