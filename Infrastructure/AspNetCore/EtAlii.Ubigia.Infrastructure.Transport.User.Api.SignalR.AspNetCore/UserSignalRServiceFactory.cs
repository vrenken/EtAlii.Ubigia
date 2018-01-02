namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.MicroContainer;

    public class UserSignalRServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, UserSignalRService>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
