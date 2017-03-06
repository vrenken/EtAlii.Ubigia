namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public interface ISystemStorageTransport : IStorageTransport
    {
        void Initialize(IStorageConnection storageConnection);

        Task Start(IStorageConnection storageConnection);
    }
}
