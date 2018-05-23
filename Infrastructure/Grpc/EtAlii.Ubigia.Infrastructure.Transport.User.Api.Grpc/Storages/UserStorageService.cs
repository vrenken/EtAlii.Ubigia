namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class UserStorageService : StorageGrpcService.StorageGrpcServiceBase, IUserStorageService
    {
        private readonly IStorageRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public UserStorageService(
            IStorageRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        public override Task<StorageSingleResponse> GetLocal(StorageSingleRequest request, ServerCallContext context)
        {
            var storage = _items.GetLocal();

            var response = new StorageSingleResponse
            {
                Storage = storage.ToWire()
            };
            return Task.FromResult(response);
        }
    }
}
