namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

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