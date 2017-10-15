namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Api;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class InfrastructureDebuggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public InfrastructureDebuggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableDebugging) // diagnostics is enabled
            {
                container.RegisterDecorator(typeof(IEntryRepository), typeof(DebuggingEntryRepositoryDecorator), Lifestyle.Singleton);
            }
        }
    }
}