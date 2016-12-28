namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    internal class TestRoot2HandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedRootHandlers { get; }

        public TestRoot2HandlerMapper()
        {
            _name = "TestRoot2";
            AllowedRootHandlers = new IRootHandler[0];
        }
    }
}
