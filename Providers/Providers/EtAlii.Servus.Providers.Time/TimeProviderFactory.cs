namespace EtAlii.Servus.Provisioning.Time
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.xTechnology.Logging;

    public class TimeProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register<IProviderConfiguration>(() => configuration);
            container.Register<IDataContext>(() => configuration.SystemDataContext);
            container.Register<IManagementConnection>(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, TimeProvider>();
            container.Register<ITimeImporter, TimeImporter> ();
            container.Register<ILogger>(() => configuration.LogFactory.Create("Time","Provider"));

            return container.GetInstance<IProvider>();
        }
    }
}
