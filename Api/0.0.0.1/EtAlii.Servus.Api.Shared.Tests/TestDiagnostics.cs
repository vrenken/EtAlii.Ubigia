namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public static class TestDiagnostics
    {
        public static IDiagnosticsConfiguration Create()
        {
            const string name = "EtAlii";
            const string category = "EtAlii.Servus.Api";

            var diagnostics = new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create(name, category),
                (factory) => factory.Create(name, category));
            return diagnostics;
        }
    }
}