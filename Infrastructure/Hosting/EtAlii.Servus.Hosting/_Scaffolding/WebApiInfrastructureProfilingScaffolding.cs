namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class WebApiInfrastructureProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public WebApiInfrastructureProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableProfiling)
            {
                container.RegisterDecorator(typeof(IComponentManager), typeof(ProfilingComponentManager), Lifestyle.Singleton);
            }
        }
    }
}