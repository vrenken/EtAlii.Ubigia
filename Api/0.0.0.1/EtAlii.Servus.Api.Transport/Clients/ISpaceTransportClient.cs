namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public interface ISpaceTransportClient
    {
        Task Connect(ISpaceConnection spaceConnection);
        Task Disconnect(ISpaceConnection spaceConnection);
    }

    public interface ISpaceTransportClient<in TTransport> : ISpaceTransportClient
        where TTransport: ISpaceTransport
    {
        Task Connect(ISpaceConnection<TTransport> spaceConnection);
        Task Disconnect(ISpaceConnection<TTransport> spaceConnection);
    }
}
