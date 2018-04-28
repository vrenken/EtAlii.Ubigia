namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public partial class EntryService : EtAlii.Ubigia.Api.Transport.Grpc.EntryGrpcService.EntryGrpcServiceBase
    {
        private readonly IEntryRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public EntryService(
            IEntryRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }
    }
}
