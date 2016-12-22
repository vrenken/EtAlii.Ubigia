namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System.Web.Http;
    using EtAlii.Servus.Api.Transport.WebApi;
    using Owin;

    public partial class WebApiUserApiComponent : IWebApiUserApiComponent
    {
        private readonly HttpConfiguration _httpConfiguration;

        public WebApiUserApiComponent(
            HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        public void Start(IAppBuilder application)
        {
            //_logger.Info("Starting WebAPI services");

            _httpConfiguration.MapHttpAttributeRoutes();
            _httpConfiguration.Formatters.Add(new PayloadMediaTypeFormatter());

            //if (_diagnostics.EnableLogging)
            //{
            //    _httpConfiguration.EnableSystemDiagnosticsTracing();
            //}

            application.UseWebApi(_httpConfiguration);

            _httpConfiguration.EnsureInitialized();

            //_logger.Info("Started WebAPI services");
        }

        public void Stop()
        {
        }
    }
}