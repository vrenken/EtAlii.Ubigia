namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System.ComponentModel;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using SimpleInjector;
    using SimpleInjector.Extensions;
    using Container = SimpleInjector.Container;

    internal class WebApiLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public WebApiLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableLogging)
            {
                container.RegisterDecorator(typeof(IAuthenticationVerifier), typeof(LoggingAuthenticationVerifier), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(IAuthenticationTokenVerifier), typeof(LoggingAuthenticationTokenVerifier), Lifestyle.Singleton);
            }
        }
    }
}