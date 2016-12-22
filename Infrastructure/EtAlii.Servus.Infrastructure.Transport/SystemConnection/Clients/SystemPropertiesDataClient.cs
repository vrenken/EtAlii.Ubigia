namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

    internal class SystemPropertiesDataClient : SystemSpaceClientBase, IPropertiesDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemPropertiesDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            await Task.Run(() => { _infrastructure.Properties.Store(identifier, properties); });
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            var result = _infrastructure.Properties.Get(identifier);
            return await Task.FromResult(result);
        }
    }
}
