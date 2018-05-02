//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
//{
//    using System.Linq;
//    using EtAlii.Ubigia.Api;
//    using EtAlii.Ubigia.Api.Transport;
//    using EtAlii.Ubigia.Infrastructure.Functional;
//    using Microsoft.Extensions.Primitives;

//    public class AuthenticationHub : Hub
//    {
//        private readonly IStorageRepository _storageRepository;

//        private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
//        private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
//	    private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

//		public AuthenticationHub(
//            ISimpleAuthenticationVerifier authenticationVerifier,
//            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier, 
//            IStorageRepository storageRepository, 
//            ISimpleAuthenticationBuilder authenticationBuilder)
//        {
//            _authenticationVerifier = authenticationVerifier;
//            _authenticationTokenVerifier = authenticationTokenVerifier;
//            _storageRepository = storageRepository;
//	        _authenticationBuilder = authenticationBuilder;
//        }

//	    public string Authenticate(string accountName, string password, string hostIdentifier)
//	    {
//		    return _authenticationVerifier.Verify(accountName, password, hostIdentifier, Role.User, Role.System);
//	    }

//	    public string AuthenticateAs(string accountName, string hostIdentifier)
//	    {
//		    Context.Connection.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
//		    var authenticationToken = stringValues.Single();
//		    _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System);

//		    return _authenticationBuilder.Build(accountName, hostIdentifier);
//		}

//		public Storage GetLocalStorage()
//        {
//            Context.Connection.GetHttpContext().Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
//            var authenticationToken = stringValues.Single();
//            _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System);

//            return _storageRepository.GetLocal();
//        }
//    }
//}

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Authentication
{
}