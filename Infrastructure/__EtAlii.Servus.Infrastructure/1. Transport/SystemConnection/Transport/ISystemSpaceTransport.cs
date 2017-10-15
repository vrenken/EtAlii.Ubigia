namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;

    public interface ISystemSpaceTransport : ISpaceTransport
    {
        Task Start(ISpaceConnection spaceConnection);
    }
}
