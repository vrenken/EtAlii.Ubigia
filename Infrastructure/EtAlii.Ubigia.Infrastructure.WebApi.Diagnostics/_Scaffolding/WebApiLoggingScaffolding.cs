namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

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
                container.RegisterDecorator(typeof(IAuthenticationVerifier), typeof(LoggingAuthenticationVerifier));
                container.RegisterDecorator(typeof(IAuthenticationTokenVerifier), typeof(LoggingAuthenticationTokenVerifier));
            }
        }
    }
}