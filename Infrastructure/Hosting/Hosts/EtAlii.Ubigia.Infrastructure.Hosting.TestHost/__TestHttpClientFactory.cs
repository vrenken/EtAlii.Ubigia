//namespace EtAlii.Ubigia.Infrastructure
//[
//    using System.Net
//    using System.Net.Http
//    using EtAlii.Ubigia.Api.Transport.WebApi

//    public class TestHttpClientFactory : IHttpClientFactory
//    [
//        private readonly TestInfrastructure _infrastructure

//        public TestHttpClientFactory(TestInfrastructure infrastructure)
//        [
//            _infrastructure = infrastructure
//        }

//        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
//        [
//            var handler = _infrastructure.Server.Handler
//            var client = new HttpClient(new TestHttpClientMessageHandler(handler, credentials, hostIdentifier, authenticationToken))

//            // Set the Accept header for BSON.
//            client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType)

//            return client
//        }
//    }
//}