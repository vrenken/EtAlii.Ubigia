namespace EtAlii.Servus.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;

    public class StorageConnectionFactory
    {
        public IStorageConnection Create(
            ITransport transport,
            IInfrastructureClient client)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new StorageConnectionScaffolding(transport), 
                new InfrastructureScaffolding(client),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            // No extensions on the Storage connection (yet).
            //foreach (var extension in configuration.Extensions)
            //{
            //    extension.Initialize(container);
            //}

            var connection = container.GetInstance<IStorageConnection>();
            return connection;
        }
    }
}
