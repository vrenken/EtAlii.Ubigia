namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class WebApiProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public WebApiProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
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