namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using global::Grpc.Core;

	public interface IGrpcTransport
    {
	    Channel Channel { get; }
	    
	    //HttpMessageHandler HttpMessageHandler { get; }

		string AuthenticationToken { get; set; }
    }
}