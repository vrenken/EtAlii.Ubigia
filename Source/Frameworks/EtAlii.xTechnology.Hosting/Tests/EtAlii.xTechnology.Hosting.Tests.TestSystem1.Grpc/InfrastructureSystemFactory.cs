namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
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

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<ISystem>();
        }
    }
}
