namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using EtAlii.xTechnology.MicroContainer;
	using global::Grpc.Core;
	using global::Grpc.Net.Client;

	public class GrpcStorageTransport : StorageTransportBase, IGrpcStorageTransport
    {
	    public GrpcChannel Channel => GetChannel();
	    private GrpcChannel _channel;

	    public Metadata AuthenticationHeaders { get; set; }
	    
		public string AuthenticationToken { get => _authenticationTokenProvider.AuthenticationToken; set => _authenticationTokenProvider.AuthenticationToken = value; }
	    private readonly IAuthenticationTokenProvider _authenticationTokenProvider;
	    private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;

        public GrpcStorageTransport(Uri address,
	        Func<Uri, GrpcChannel> grpcChannelFactory, 
	        IAuthenticationTokenProvider authenticationTokenProvider)
	        : base(address)
        {
	        _grpcChannelFactory = grpcChannelFactory;
	        _authenticationTokenProvider = authenticationTokenProvider;
        }

	    /// <summary>
	    /// Gets a channel based on the specified Uri. 
	    /// </summary>
	    /// <returns></returns>
	    private GrpcChannel GetChannel()
	    {
		    var uriAsString = _channel?.Target;
		    var hasAddress = !string.IsNullOrWhiteSpace(uriAsString);
		    if (hasAddress)
		    {
			    var channelAddress = new Uri($"http://{uriAsString}");

			    var hasSameHost = string.Equals(Address.DnsSafeHost, channelAddress.DnsSafeHost, StringComparison.InvariantCultureIgnoreCase);
			    var hasSamePort = Address.Port == channelAddress.Port;
			    if (hasSameHost && hasSamePort)
			    {
				    return _channel;
			    }
		    }
		    return _channel = _grpcChannelFactory.Invoke(Address);
	    }

        public override async Task Stop()
        {
            await base.Stop();

	        _channel?.Dispose();
	        _channel = null;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new GrpcStorageClientsScaffolding()
            };
        }
    }
}
