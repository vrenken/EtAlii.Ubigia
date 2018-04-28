namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class StorageService : EtAlii.Ubigia.Api.Transport.Management.Grpc.StorageGrpcService.StorageGrpcServiceBase
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
