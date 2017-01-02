namespace EtAlii.Servus.Hosting.WebsiteHost
{
    using EtAlii.Servus.Hosting.Owin;
    using EtAlii.Servus.Infrastructure;

    public class WebsiteHostFactory : OwinHostedHostFactory<WebsiteHost>
    {
        public WebsiteHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
