namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class TestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedRootHandlers { get { return _allowedRootHandlers; } }
        private readonly IRootHandler[] _allowedRootHandlers;

        public TestRootHandlerMapper()
        {
            _name = "TestRoot";
            _allowedRootHandlers = new IRootHandler[0];
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
