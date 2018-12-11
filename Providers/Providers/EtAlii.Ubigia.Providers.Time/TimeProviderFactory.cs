namespace EtAlii.Ubigia.Provisioning.Time
{
    public class TimeProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new xTechnology.MicroContainer.Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemScriptContext);
            container.Register(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, TimeProvider>();
            container.Register<ITimeImporter, TimeImporter> ();
            container.Register(() => configuration.LogFactory.Create("Time","Provider"));

            return container.GetInstance<IProvider>();
        }
    }
}
