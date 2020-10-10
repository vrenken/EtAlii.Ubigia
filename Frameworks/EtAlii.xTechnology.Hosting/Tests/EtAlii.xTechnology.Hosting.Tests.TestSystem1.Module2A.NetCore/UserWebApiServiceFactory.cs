namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserWebApiServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, UserWebApiService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IService>();
        }
    }
}
