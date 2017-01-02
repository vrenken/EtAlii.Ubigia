namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class TestRootHandler : IRootHandler
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootSubHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootSubHandler[] _allowedPaths;

        public TestRootHandler()
        {
            _name = "TestRoot";
            _allowedPaths = new IRootSubHandler[0];
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
