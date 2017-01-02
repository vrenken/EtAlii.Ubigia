namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.xTechnology.MicroContainer;

    public class ManagementConnectionScaffolding : IScaffolding
    {
        private readonly IManagementConnectionConfiguration _configuration;

        public ManagementConnectionScaffolding(IManagementConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IManagementConnectionConfiguration>(() => _configuration);
            container.Register<IManagementConnection, ManagementConnection>();
        }
    }
}
