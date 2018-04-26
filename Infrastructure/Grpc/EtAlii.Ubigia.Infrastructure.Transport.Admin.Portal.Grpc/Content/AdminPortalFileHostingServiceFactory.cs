namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.MicroContainer;

    public class AdminPortalFileHostingServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, AdminPortalFileHostingService>();

            return container.GetInstance<IService>();
        }
    }
}
