namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System.Net.Http;

	public interface IGrpcTransport
    {
	    HttpMessageHandler HttpMessageHandler { get; }

		string AuthenticationToken { get; set; }
    }
}