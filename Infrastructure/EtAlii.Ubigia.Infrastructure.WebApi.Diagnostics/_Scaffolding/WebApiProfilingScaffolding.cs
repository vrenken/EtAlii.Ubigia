namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using SimpleInjector;
    using Container = SimpleInjector.Container;

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
                container.RegisterDecorator(typeof(IWebApiComponentManager), typeof(ProfilingWebApiComponentManager), Lifestyle.Singleton);
            }
        }
    }
}