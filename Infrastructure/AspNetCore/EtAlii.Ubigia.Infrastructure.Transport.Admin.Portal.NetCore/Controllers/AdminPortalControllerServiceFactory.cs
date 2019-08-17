namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.NetCore
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalControllerServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, AdminPortalControllerService>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
