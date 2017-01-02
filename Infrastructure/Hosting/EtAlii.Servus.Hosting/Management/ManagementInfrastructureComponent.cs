namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;

    public class ManagementInfrastructureComponent : InfrastructureComponent
    {
        public override void Setup(IAppBuilder application, HttpConfiguration webApiConfiguration)
        {
            webApiConfiguration.Routes.MapHttpRoute
            (
                name: "ManagementRoute",
                routeTemplate: "management/{controller}"
            );
        }
    }
}