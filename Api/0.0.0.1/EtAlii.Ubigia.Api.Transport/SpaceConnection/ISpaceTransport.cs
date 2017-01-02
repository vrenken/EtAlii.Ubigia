namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public interface ISpaceTransport
    {
        bool IsConnected { get; }

        void Initialize(ISpaceConnection spaceConnection, string address);

        Task Start(ISpaceConnection spaceConnection, string address);
        Task Stop(ISpaceConnection spaceConnection);
        
        IScaffolding[] CreateScaffolding();
    }
}
