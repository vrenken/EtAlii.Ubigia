namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.Threading;

    public class AdminRestServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, AdminRestService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);
            container.Register<IContextCorrelator, ContextCorrelator>();

            return container.GetInstance<IService>();
        }
    }
}
