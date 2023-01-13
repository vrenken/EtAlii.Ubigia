// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System;
using System.Threading.Tasks;
using EtAlii.xTechnology.MicroContainer;
using EtAlii.xTechnology.Threading;
using global::Grpc.Core;
using global::Grpc.Net.Client;
using global::Grpc.Core.Interceptors;

public class GrpcSpaceTransport : SpaceTransportBase, IGrpcSpaceTransport
{
    public CallInvoker CallInvoker => GetCallInvoker();
    private CallInvoker _callInvoker;
    private GrpcChannel _channel;

    public Metadata.Entry AuthenticationHeader { get; set; }

    public string AuthenticationToken { get => _authenticationTokenProvider.AuthenticationToken; set => _authenticationTokenProvider.AuthenticationToken = value; }
    private readonly IAuthenticationTokenProvider _authenticationTokenProvider;
    private readonly IContextCorrelator _contextCorrelator;
    private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;

    public GrpcSpaceTransport(
        Uri address,
        Func<Uri, GrpcChannel> grpcChannelFactory,
        IAuthenticationTokenProvider authenticationTokenProvider,
        IContextCorrelator contextCorrelator)
        : base(address)
    {
        _grpcChannelFactory = grpcChannelFactory;
        _authenticationTokenProvider = authenticationTokenProvider;
        _contextCorrelator = contextCorrelator;
    }

    private CallInvoker GetCallInvoker()
    {
        var uriAsString = _channel?.Target;
        var hasAddress = !string.IsNullOrWhiteSpace(uriAsString);
        if (hasAddress)
        {
            var channelAddress = new Uri($"https://{uriAsString}");

            var hasSameHost = string.Equals(Address.DnsSafeHost, channelAddress.DnsSafeHost, StringComparison.InvariantCultureIgnoreCase);
            var hasSamePort = Address.Port == channelAddress.Port;
            if (hasSameHost && hasSamePort)
            {
                return _callInvoker;
            }
        }

        _channel = _grpcChannelFactory.Invoke(Address);
        _callInvoker = _channel.Intercept(new CorrelationCallInterceptor(_contextCorrelator));
        return _callInvoker;
    }

    public override async Task Stop()
    {
        await base
            .Stop()
            .ConfigureAwait(false);

        _channel?.Dispose();
        _channel = null;
    }

    protected override IScaffolding[] CreateScaffoldingInternal(SpaceConnectionOptions spaceConnectionOptions)
    {
        return new IScaffolding[]
        {
            new GrpcSpaceClientsScaffolding(spaceConnectionOptions)
        };
    }
}
