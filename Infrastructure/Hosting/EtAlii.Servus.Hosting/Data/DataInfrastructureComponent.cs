namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;

    public class DataInfrastructureComponent : InfrastructureComponent
    {
        public override void Setup(IAppBuilder application, HttpConfiguration configuration)
        {
            //configuration.Routes.MapHttpRoute
            //(
            //    name: "RetrievalRoute", 
            //    routeTemplate: "retrieval/{controller}" 
            //);
        }
    }
}