namespace EtAlii.Servus.Infrastructure.Hosting
{
    using Owin;
    using System.Web.Http;
    using System.Web.Mvc;

    public class FilterComponent : InfrastructureComponent
    {
        public override void Setup(IAppBuilder application, HttpConfiguration configuration)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            //GlobalFilters.Filters.Add(new HttpsAttribute());
            //GlobalFilters.Filters.Add(new AuthenticationFilter());
            //GlobalFilters.Filters.Add(new RequiresAuthenticationTokenAttribute());
        }
    }
}