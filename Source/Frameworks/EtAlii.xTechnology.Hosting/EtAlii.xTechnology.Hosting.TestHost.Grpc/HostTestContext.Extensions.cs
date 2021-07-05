// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
    using Grpc.Core;
	using Grpc.Net.Client;

	public static class HostTestContextExtensions
    {
	    public static TGrpcClient CreateClient<TGrpcClient>(this IHostTestContext context, string address, Func<GrpcChannel, TGrpcClient> construct)
		    where TGrpcClient: ClientBase
	    {
#pragma warning disable CA2000 // This channel's lifetime will be controlled by the client that will consume it.
		    var channel = CreateChannel(context, address);
		    return construct(channel);
#pragma warning restore CA2000
	    }

	    public static GrpcChannel CreateChannel(this IHostTestContext context, Uri address) => CreateChannel(context, address.ToString());

	    public static GrpcChannel CreateChannel(this IHostTestContext context, string address)
	    {
		    var options = new GrpcChannelOptions
		    {
#pragma warning disable CA1416
			    HttpClient = context.CreateClient(),
#pragma warning restore CA1416
			    Credentials = ChannelCredentials.Insecure,
			    DisposeHttpClient = true,
		    };
		    var channel = GrpcChannel.ForAddress(address, options);
		    return channel;
	    }
    }
}
