namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class UserPropertiesService : PropertiesGrpcService.PropertiesGrpcServiceBase, IUserPropertiesService
    {
        private readonly IPropertiesRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public UserPropertiesService(
            IPropertiesRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        public override Task<PropertiesGetResponse> Get(PropertiesGetRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var propertyDictionary = _items.Get(entryId);

            var response = new PropertiesGetResponse
            {
                PropertyDictionary = propertyDictionary.ToWire()
            };
            return Task.FromResult(response);
        }

        public override Task<PropertiesPostResponse> Post(PropertiesPostRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var propertyDictionary = request.PropertyDictionary.ToLocal();
            _items.Store(entryId, propertyDictionary);

            var response = new PropertiesPostResponse();
            return Task.FromResult(response);
        }
    }
}
