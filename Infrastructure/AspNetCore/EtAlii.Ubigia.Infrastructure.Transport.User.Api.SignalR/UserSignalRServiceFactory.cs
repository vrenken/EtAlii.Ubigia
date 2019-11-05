namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserSignalRServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, UserSignalRService>();

            container.Register(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
