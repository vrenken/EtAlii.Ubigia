namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.NetCore
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserPortalFileHostingServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, UserPortalFileHostingService>();

            container.Register(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
