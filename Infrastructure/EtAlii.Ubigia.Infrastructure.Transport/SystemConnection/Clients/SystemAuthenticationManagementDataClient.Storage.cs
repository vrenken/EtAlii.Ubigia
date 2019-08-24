namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationManagementDataClient
    {
        public Task<Storage> GetConnectedStorage(ISpaceConnection connection, string address)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var storage = _infrastructure.Storages.GetLocal();

            // We do not want the address pushed to us from the server. 
            // If we get here then we already know how to contact the server. 
            storage.Address = address;

            return Task.FromResult(storage);
        }
        public Task<Storage> GetConnectedStorage(IStorageConnection storageConnection)
        {
            if (storageConnection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var storage = _infrastructure.Storages.GetLocal();

            // We do not want the address pushed to us from the server. 
            // If we get here then we already know how to contact the server. 
            storage.Address = storageConnection.Transport.Address.ToString();

            return Task.FromResult(storage);
        }
    }
}
