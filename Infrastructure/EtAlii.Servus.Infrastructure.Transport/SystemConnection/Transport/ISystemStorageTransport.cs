namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;

    public interface ISystemStorageTransport : IStorageTransport
    {
        void Initialize(IStorageConnection storageConnection);

        Task Start(IStorageConnection storageConnection);
    }
}
