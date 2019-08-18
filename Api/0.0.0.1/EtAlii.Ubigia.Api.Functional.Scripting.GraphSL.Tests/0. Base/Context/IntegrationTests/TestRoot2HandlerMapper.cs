namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    internal class TestRoot2HandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public TestRoot2HandlerMapper()
        {
            Name = "TestRoot2";
            AllowedRootHandlers = new IRootHandler[0];
        }
    }
}
