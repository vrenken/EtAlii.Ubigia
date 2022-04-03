// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
    using System.Collections.Generic;
    using Grpc.Core;
	using Grpc.Net.Client;
    using Grpc.Net.Compression;
    using CompressionLevel = System.IO.Compression.CompressionLevel;

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
                CompressionProviders = new List<ICompressionProvider> { new GzipCompressionProvider(CompressionLevel.NoCompression) },
                HttpHandler = context.CreateHandler(),
// #pragma warning disable CA1416
// 			    HttpClient = context.CreateClient(),
// #pragma warning restore CA1416
		    };
		    var channel = GrpcChannel.ForAddress(address, options);
		    return channel;
	    }
    }
}
