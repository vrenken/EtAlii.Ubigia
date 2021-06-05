namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserRestServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, UserRestService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IService>();
        }
    }
}
