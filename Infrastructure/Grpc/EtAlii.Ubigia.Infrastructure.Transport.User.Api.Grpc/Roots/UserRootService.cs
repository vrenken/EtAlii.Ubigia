namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Roots
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class UserRootService : RootGrpcService.RootGrpcServiceBase, IUserRootService
    {
        private readonly IRootRepository _items;
        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

        public UserRootService(
            IRootRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
        {
            _items = items;
            _authenticationTokenVerifier = authenticationTokenVerifier;
        }

        public override Task<RootResponse> Get(RootRequest request, ServerCallContext context)
        {
            return base.Get(request, context);
        }

        public override Task<RootResponse> Post(RootRequest request, ServerCallContext context)
        {
            return base.Post(request, context);
        }

        public override Task<RootResponse> Put(RootRequest request, ServerCallContext context)
        {
            return base.Put(request, context);
        }

        public override Task<RootResponse> Delete(RootRequest request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }
    }
}
