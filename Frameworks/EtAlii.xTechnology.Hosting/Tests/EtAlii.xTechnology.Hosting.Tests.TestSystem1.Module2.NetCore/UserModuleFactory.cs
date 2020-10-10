namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserModuleFactory : ModuleFactoryBase
    {
        public override IModule Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IModule, UserModule>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IModule>();
        }
    }
}
