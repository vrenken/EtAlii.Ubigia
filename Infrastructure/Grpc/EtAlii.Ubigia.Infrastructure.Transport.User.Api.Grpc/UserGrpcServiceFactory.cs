namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.MicroContainer;

    public class UserGrpcServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, UserGrpcService>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
