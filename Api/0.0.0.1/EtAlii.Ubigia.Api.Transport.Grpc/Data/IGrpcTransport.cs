namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using global::Grpc.Core;

	public interface IGrpcTransport
    {
	    Channel Channel { get; }
	    
		string AuthenticationToken { get; set; }
	    
	    Metadata AuthenticationHeaders { get; set; }
    }
}