namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using SimpleInjector;
    using EtAlii.xTechnology.Diagnostics;
    using IScaffolding = EtAlii.Servus.Infrastructure.Hosting.IScaffolding;

    public class TestHostExtension : IHostExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public TestHostExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TestHostScaffolding(),
                new TestHostProfilingScaffolding(_diagnostics),
                new TestHostLoggingScaffolding(_diagnostics),
                new TestHostDebuggingScaffolding(_diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}