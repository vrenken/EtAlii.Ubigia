namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.MicroContainer;

    public class AdminGrpcServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, AdminGrpcService>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
