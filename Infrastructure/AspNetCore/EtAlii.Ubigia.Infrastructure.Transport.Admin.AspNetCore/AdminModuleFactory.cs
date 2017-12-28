namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.AspNetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminModuleFactory : ModuleFactoryBase
    {
        public override IModule Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IModule, AdminModule>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IModule>();
        }
    }
}
