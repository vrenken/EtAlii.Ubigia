namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System.Net;
    using System.Net.Http;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using Microsoft.AspNetCore.TestHost;

	internal class TestHttpClientFactory : IHttpClientFactory
	{
        private readonly TestServer _testServer;

        public TestHttpClientFactory(TestServer testServer)
        {
			_testServer = testServer;
        }

        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
			var handler = _testServer.CreateHandler();
			var client = new HttpClient(new TestHttpClientMessageHandler(handler, credentials, hostIdentifier, authenticationToken));

	  //      var client = _testServer.CreateClient();
			//if (credentials != null)
	  //      {
		 //       var cr = credentials.GetCredential(_testServer.BaseAddress, "Basic-Authentication");
		 //       client.DefaultRequestHeaders.Add("Test-UserName", cr.UserName);
		 //       client.DefaultRequestHeaders.Add("Test-Password", cr.Password);
	  //      }

	  //      client.DefaultRequestHeaders.Add("Host-Identifier", hostIdentifier);
	  //      client.DefaultRequestHeaders.Add("Authentication-Token", authenticationToken);


			// Set the Accept header for BSON.
			client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType);

            return client;
        }
    }
}