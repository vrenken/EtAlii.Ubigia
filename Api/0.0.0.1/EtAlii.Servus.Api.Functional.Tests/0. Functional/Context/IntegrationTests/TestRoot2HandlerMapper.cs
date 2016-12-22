namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class TestRoot2HandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootHandler[] _allowedPaths;

        public TestRoot2HandlerMapper()
        {
            _name = "TestRoot2";
            _allowedPaths = new IRootHandler[0];
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
