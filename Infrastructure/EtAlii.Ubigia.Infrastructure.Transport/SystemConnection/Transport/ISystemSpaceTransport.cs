namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public interface ISystemSpaceTransport : ISpaceTransport
    {
        Task Start(ISpaceConnection spaceConnection);
    }
}
