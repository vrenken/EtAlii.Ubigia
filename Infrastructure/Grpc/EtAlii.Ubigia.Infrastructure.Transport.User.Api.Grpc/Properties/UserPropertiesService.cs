namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Properties
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;

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

    }
}
