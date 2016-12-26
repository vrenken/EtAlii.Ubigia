namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class InvalidTestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }

        public IRootHandler[] AllowedRootHandlers { get { return _allowedRootHandlers; } }
        private readonly IRootHandler[] _allowedRootHandlers;

        private readonly string _name;

        public InvalidTestRootHandlerMapper()
        {
            _name = "TestRoot";
            _allowedRootHandlers = new IRootHandler[]
            {
                new TimeRootByPathBasedYyyymmddhhmmssHandler(),
                new TimeRootByPathBasedYyyymmddhhmmssHandler(),
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
