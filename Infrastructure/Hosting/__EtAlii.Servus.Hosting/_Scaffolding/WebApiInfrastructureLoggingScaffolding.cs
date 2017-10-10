namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Web.Http;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.WebApi;

    internal class WebApiInfrastructureLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public WebApiInfrastructureLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
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