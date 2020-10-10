namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class StorageServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, StorageService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IService>();
        }
    }
}
