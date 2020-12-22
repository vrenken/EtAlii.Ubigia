namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;

    internal class TestRoot2HandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public TestRoot2HandlerMapper()
        {
            Name = "TestRoot2";
            AllowedRootHandlers = Array.Empty<IRootHandler>();
        }
    }
}
