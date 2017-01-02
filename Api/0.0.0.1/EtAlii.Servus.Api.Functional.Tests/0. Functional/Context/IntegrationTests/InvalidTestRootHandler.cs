namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class InvalidTestRootHandler : IRootHandler
    {
        public string Name { get { return _name; } }

        public IRootSubHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootSubHandler[] _allowedPaths;

        private readonly string _name;

        public InvalidTestRootHandler()
        {
            _name = "TestRoot";
            _allowedPaths = new IRootSubHandler[]
            {
                new TimeRootByPathBasedYYYYMMDDHHMMSSSubHandler(),
                new TimeRootByPathBasedYYYYMMDDHHMMSSSubHandler(),
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
