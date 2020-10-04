namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using global::Grpc.Core;
	using global::Grpc.Net.Client;

	public interface IGrpcTransport
    {
	    GrpcChannel Channel { get; }
	    
		string AuthenticationToken { get; set; }
	    
	    Metadata AuthenticationHeaders { get; set; }
    }
}