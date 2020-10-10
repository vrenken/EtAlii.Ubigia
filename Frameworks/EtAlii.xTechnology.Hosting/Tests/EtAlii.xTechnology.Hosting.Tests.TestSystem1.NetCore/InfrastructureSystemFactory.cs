namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureSystemFactory : SystemFactoryBase
    {
        public override ISystem Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<ISystem, InfrastructureSystem>();
            container.Register<ISystemCommandsFactory, SystemCommandsFactory>();

            return container.GetInstance<ISystem>();
        }
    }
}
