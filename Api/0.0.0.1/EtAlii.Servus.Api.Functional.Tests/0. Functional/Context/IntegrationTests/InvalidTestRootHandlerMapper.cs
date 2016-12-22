namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class InvalidTestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }

        public IRootHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootHandler[] _allowedPaths;

        private readonly string _name;

        public InvalidTestRootHandlerMapper()
        {
            _name = "TestRoot";
            _allowedPaths = new IRootHandler[]
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
