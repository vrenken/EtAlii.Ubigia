namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Api;
    using SimpleInjector;
    using SimpleInjector.Extensions;

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