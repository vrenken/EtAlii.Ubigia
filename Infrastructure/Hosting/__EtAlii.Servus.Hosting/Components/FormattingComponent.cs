namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;

    public class FormattingInfrastructureComponent : InfrastructureComponent
    {
        public override void Setup(IAppBuilder application, HttpConfiguration webApiConfiguration)
        {
            //var appXmlType = webApiConfiguration.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //webApiConfiguration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            webApiConfiguration.Formatters.Add(new BsonMediaTypeFormatter());

            webApiConfiguration.EnableSystemDiagnosticsTracing();
        }
    }
}