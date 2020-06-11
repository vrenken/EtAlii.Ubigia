namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalFileHostingServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register(() => configuration);
            container.Register<IService, AdminPortalFileHostingService>();

            return container.GetInstance<IService>();
        }
    }
}
