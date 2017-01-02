namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public interface IStorageTransport
    {
        bool IsConnected { get; }

        void Initialize(IStorageConnection storageConnection, string address);

        Task Start(IStorageConnection storageConnection, string address);

        Task Stop(IStorageConnection storageConnection);
        
        IScaffolding[] CreateScaffolding();
    }
}
