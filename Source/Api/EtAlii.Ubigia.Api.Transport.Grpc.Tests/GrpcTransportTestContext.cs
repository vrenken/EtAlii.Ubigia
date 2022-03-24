// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc.Tests
{
    using System;
    using EtAlii.xTechnology.Threading;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using global::Grpc.Net.Client;

    public sealed class GrpcTransportTestContext : TransportTestContextBase<InfrastructureHostTestContext>
    {
        protected override ITransportProvider CreateTransportProvider(IContextCorrelator contextCorrelator)
        {
            var grpcChannelFactory = new Func<Uri, GrpcChannel>(channelAddress => Host.CreateGrpcInfrastructureChannel(channelAddress));
            return GrpcTransportProvider.Create(grpcChannelFactory, contextCorrelator);
        }
        protected override IStorageTransportProvider CreateStorageTransportProvider(IContextCorrelator contextCorrelator)
        {
            var grpcChannelFactory = new Func<Uri, GrpcChannel>((channelAddress) => Host.CreateGrpcInfrastructureChannel(channelAddress));
            return GrpcStorageTransportProvider.Create(grpcChannelFactory, contextCorrelator);
        }
    }
}
