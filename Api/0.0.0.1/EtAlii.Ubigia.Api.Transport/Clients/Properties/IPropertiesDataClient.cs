namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IPropertiesDataClient : ISpaceTransportClient
    {
        Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope);
        Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope);
    }

    public interface IPropertiesDataClient<in TTransport> : IPropertiesDataClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
