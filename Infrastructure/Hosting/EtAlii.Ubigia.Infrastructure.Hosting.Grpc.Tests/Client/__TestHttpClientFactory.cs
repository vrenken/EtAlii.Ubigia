//namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
//{
//	using System.Net
//    using System.Net.Http
//    using EtAlii.Ubigia.Api.Transport.Grpc
////    using Microsoft.Grpc.TestHost
//
//	internal class TestHttpClientFactory : IHttpClientFactory
//	{
//        private readonly TestServer _testServer
//
//        public TestHttpClientFactory(TestServer testServer)
//        {
//			_testServer = testServer
//        }
//
//        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
//        {
//			var handler = _testServer.CreateHandler()
//			var client = new HttpClient(new TestHttpClientMessageHandler(handler, credentials, hostIdentifier, authenticationToken))
//
//	        // Set the Accept header for BSON.
//	        client.DefaultRequestHeaders.Accept.Clear()
//			client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType)
//
//			return client
//        }
//    }
//}