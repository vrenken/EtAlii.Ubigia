namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class PropertiesService : EtAlii.Ubigia.Api.Transport.Grpc.PropertiesGrpcService.PropertiesGrpcServiceBase
    {
        private readonly IPropertiesRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public PropertiesService(
            IPropertiesRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

    }
}
