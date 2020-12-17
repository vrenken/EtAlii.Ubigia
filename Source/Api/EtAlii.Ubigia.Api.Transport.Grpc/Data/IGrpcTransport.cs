namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using global::Grpc.Core;

	public interface IGrpcTransport
    {
        /// <summary>
        /// We want to forward a CallInvoker instead of a GrpcChannel.
        /// Reason is that a CallInvoker works with Interceptors, whereas the GrpcChannel does not.
        /// </summary>
	    CallInvoker CallInvoker { get; }

		string AuthenticationToken { get; set; }

	    Metadata AuthenticationHeaders { get; set; }
    }
}
