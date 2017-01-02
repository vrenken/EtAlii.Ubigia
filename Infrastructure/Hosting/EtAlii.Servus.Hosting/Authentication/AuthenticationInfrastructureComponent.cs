namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;

    public class AuthenticationInfrastructureComponent : InfrastructureComponent
    {
        public override void Setup(IAppBuilder application, HttpConfiguration webApiConfiguration)
        {
            webApiConfiguration.Routes.MapHttpRoute
            (
                name: "AuthenticationRoute",
                routeTemplate: "{controller}" 
            );
        }
    }
}