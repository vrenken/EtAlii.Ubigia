namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IInfrastructureService, InfrastructureService>();
            container.Register<IServiceDetailsBuilder, ServiceDetailsBuilder>();
            
            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IInfrastructureService>();
        }
    }
}
