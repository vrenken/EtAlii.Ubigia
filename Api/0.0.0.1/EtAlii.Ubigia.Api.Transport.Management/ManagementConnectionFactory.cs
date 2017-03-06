namespace EtAlii.Ubigia.Api.Transport.Management
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public sealed class ManagementConnectionFactory : IManagementConnectionFactory
    {
        public IManagementConnection Create(IManagementConnectionConfiguration configuration)
        {
            var factoryMethod = configuration.FactoryExtension ?? (() => CreateInternal(configuration));
            return factoryMethod();
        }

        private IManagementConnection CreateInternal(IManagementConnectionConfiguration configuration)
        {
            var hasTransportProvider = configuration.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating management connection: No TransportProvider provided.");
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionScaffolding(configuration)
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IManagementConnection>();
        }
    }
}
