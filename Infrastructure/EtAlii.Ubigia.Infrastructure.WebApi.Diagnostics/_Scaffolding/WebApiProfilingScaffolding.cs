namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

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
                container.RegisterDecorator(typeof(IWebApiComponentManager), typeof(ProfilingWebApiComponentManager));
            }
        }
    }
}