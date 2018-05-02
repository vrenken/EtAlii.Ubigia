namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Storages
{
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class StorageService : StorageGrpcService.StorageGrpcServiceBase
    {
        private readonly IStorageRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public StorageService(
            IStorageRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }
    }
}
