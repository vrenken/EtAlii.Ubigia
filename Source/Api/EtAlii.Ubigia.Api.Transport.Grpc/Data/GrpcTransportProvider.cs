// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System;
using global::Grpc.Net.Client;
using EtAlii.xTechnology.Threading;

public class GrpcTransportProvider : ITransportProvider
{
    private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;
    private readonly IContextCorrelator _contextCorrelator;

    private GrpcTransportProvider(Func<Uri, GrpcChannel> grpcChannelFactory, IContextCorrelator contextCorrelator)
    {
        _grpcChannelFactory = grpcChannelFactory;
        _contextCorrelator = contextCorrelator;
    }

    public ISpaceTransport GetSpaceTransport(Uri address)
    {
        // We always want to use a new authenticationTokenProvider.
        var authenticationTokenProvider = new AuthenticationTokenProvider();
        return new GrpcSpaceTransport(address, _grpcChannelFactory, authenticationTokenProvider, _contextCorrelator);
    }

    public static GrpcTransportProvider Create(Func<Uri, GrpcChannel> channelFactory, IContextCorrelator contextCorrelator)
    {
        return new(channelFactory, contextCorrelator);
    }

    public static GrpcTransportProvider Create(IContextCorrelator contextCorrelator)
    {
        var channelFactory = new Func<Uri, GrpcChannel>(channelAddress =>
        {
            var options = new GrpcChannelOptions();
            return GrpcChannel.ForAddress(channelAddress, options);
        });
        return new GrpcTransportProvider(channelFactory, contextCorrelator);
    }
}
