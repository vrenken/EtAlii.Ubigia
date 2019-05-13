//namespace EtAlii.Ubigia.Provisioning
//[
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Transport
//    using EtAlii.Ubigia.Api.Transport.Management
//    using EtAlii.Ubigia.Api.Transport.Management.SignalR
//    using EtAlii.Ubigia.Api.Transport.SignalR
//    using EtAlii.xTechnology.MicroContainer
//    using xTechnology.Hosting

//    public class ProvisioningExtension : IProviderExtension
//    [
//        private readonly IProvisioningConfiguration _configuration

//        public ProvisioningExtension(IProvisioningConfiguration configuration)
//        [
//            _configuration = configuration
//        ]
//        public void Register(Container container)
//        [
//            //container.Register<IProviderHost, TProvider>()
//            container.Register(() => _configuration)
//            container.Register<IProvisioningService, ProvisioningService>()

//            container.Register(() =>
//            [
//                var configuration = _configuration.CreateManagementConnectionConfiguration()
//                    .Use(SignalRStorageTransportProvider.Create())
//                    .Use(_configuration.Address)
//                    .Use(_configuration.Account, _configuration.Password)
//                var connection = new ManagementConnectionFactory().Create(configuration)
//                var task = Task. Run(async () =>
//                [
//                    await connection.Open()
//                ])
//                task.Wait[]
//                return connection
//            ])
//            container.Register(() =>
//            [
//                var configuration = _configuration.CreateDataConnectionConfiguration()
//                    .Use(SignalRTransportProvider.Create())
//                    .Use(_configuration.Address)
//                    .Use(_configuration.Account, SpaceName.System, _configuration.Password)
//                var connection = new DataConnectionFactory().Create(configuration)

//                var task = Task. Run(async () =>
//                [
//                    await connection.Open()
//                ])
//                task.Wait[]

//                return _configuration.CreateDataContext(connection)
//            ])
//        ]
//    ]
//]