namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface ISpaceTransportClient
    {
        Task Connect(ISpaceConnection spaceConnection);
        Task Disconnect(); 
    }

    public interface ISpaceTransportClient<in TTransport> : ISpaceTransportClient
        where TTransport: ISpaceTransport
    {
        Task Connect(ISpaceConnection<TTransport> spaceConnection);
    }
}
