namespace EtAlii.Ubigia.Infrastructure.Transport.User.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserModuleFactory : ModuleFactoryBase
    {
        public override IModule Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IModule, UserModule>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IModule>();
        }
    }
}
