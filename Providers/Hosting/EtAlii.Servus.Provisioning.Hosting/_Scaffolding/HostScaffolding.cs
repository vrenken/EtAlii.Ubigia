namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Management.SignalR;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    internal class HostScaffolding<TProvider> : IScaffolding
        where TProvider : class, IProviderHost
    {
        private readonly IHostConfiguration _configuration;

        public HostScaffolding(
            IHostConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IProviderHost, TProvider>();
            container.Register<IHostConfiguration>(() => _configuration);

            container.Register<IManagementConnection>(() =>
            {
                var configuration = _configuration.CreateManagementConnectionConfiguration()
                .Use(SignalRStorageTransportProvider.Create())
                .Use(_configuration.Address)
                .Use(_configuration.Account, _configuration.Password);
                var connection = new ManagementConnectionFactory().Create(configuration);
                var task = Task.Run(async () =>
                {
                    await connection.Open();
                });
                task.Wait();
                return connection;
            });
            container.Register<IDataContext>(() =>
            {
                var configuration = _configuration.CreateDataConnectionConfiguration()
                    .Use(SignalRTransportProvider.Create())
                    .Use(_configuration.Address)
                    .Use(_configuration.Account, SpaceName.System, _configuration.Password);
                var connection = new DataConnectionFactory().Create(configuration);

                var task = Task.Run(async () =>
                {
                    await connection.Open();
                });
                task.Wait();

                return _configuration.CreateDataContext(connection);
            });
        }
    }
}