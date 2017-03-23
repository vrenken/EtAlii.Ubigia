namespace EtAlii.Ubigia.Api.Functional.Tests
{
    internal class TestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public TestRootHandlerMapper()
        {
            Name = "TestRoot";
            AllowedRootHandlers = new IRootHandler[0];
        }
    }
}
