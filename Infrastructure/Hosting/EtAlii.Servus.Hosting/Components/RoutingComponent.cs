namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;

    public class RoutingComponent : InfrastructureComponent
    {
        public override void Setup(IAppBuilder application, HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }
    }
}