﻿namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.MicroContainer;

    //using EtAlii.Ubigia.Api.Transport.Management.SignalR
    //using EtAlii.Ubigia.Api.Transport.SignalR

    internal class ProvisioningScaffolding2 : IScaffolding
    {
        private readonly IProvisioningConfiguration _configuration;

        public ProvisioningScaffolding2(IProvisioningConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IProvisioning, Provisioning>();
            container.Register(() => _configuration);

            container.Register(() =>
            {
                var configuration = _configuration.CreateManagementConnectionConfiguration()
                .Use(_configuration.CreateStorageTransportProvider()) //WAS: .Use(SignalRStorageTransportProvider.Create())
                .Use(_configuration.Address)
                .Use(_configuration.Account, _configuration.Password);
                var connection = new ManagementConnectionFactory().Create(configuration);
                connection.Open().Wait();
                return connection;
            });
            container.Register(() =>
            {
                var configuration = _configuration.CreateDataConnectionConfiguration()
                    .UseTransport(_configuration.CreateTransportProvider()) // WAS: .Use(SignalRTransportProvider.Create())
                    .Use(_configuration.Address)
                    .Use(_configuration.Account, SpaceName.System, _configuration.Password);
                var connection = new DataConnectionFactory().Create(configuration);
                connection.Open().Wait();
                return _configuration.CreateScriptContext(connection);
            });
        }
    }
}