namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IInfrastructureService, InfrastructureService>();
            container.Register<IConfiguration>(() => configuration);

            return container.GetInstance<IInfrastructureService>();
        }
    }
}
