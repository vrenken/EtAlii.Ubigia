namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Portal.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalControllerServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, AdminPortalControllerService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IService>();
        }
    }
}
