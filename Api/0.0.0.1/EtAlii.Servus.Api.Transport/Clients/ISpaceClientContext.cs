namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public interface ISpaceClientContext
    {
        Task Open(ISpaceConnection spaceConnection);
        Task Close(ISpaceConnection spaceConnection);
    }
}