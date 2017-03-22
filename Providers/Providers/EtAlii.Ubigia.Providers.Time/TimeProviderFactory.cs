namespace EtAlii.Ubigia.Provisioning.Time
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public class TimeProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemDataContext);
            container.Register(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, TimeProvider>();
            container.Register<ITimeImporter, TimeImporter> ();
            container.Register(() => configuration.LogFactory.Create("Time","Provider"));

            return container.GetInstance<IProvider>();
        }
    }
}
