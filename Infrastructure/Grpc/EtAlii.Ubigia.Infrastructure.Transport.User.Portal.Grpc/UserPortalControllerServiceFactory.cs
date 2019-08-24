namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserPortalControllerServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, UserPortalControllerService>();

            container.Register(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
