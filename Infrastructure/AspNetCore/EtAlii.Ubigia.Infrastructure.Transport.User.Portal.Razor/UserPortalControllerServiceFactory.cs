namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserPortalControllerServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, UserPortalControllerService>();

            container.Register(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
