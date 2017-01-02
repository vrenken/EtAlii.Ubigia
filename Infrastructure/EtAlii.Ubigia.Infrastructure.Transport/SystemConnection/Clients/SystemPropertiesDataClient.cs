namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

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
