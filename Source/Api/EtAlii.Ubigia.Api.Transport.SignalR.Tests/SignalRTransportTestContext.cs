// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
	using System;
	using System.Net.Http;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;
	using EtAlii.Ubigia.Api.Transport.Tests;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Threading;

	public class SignalRTransportTestContext : TransportTestContextBase<InfrastructureHostTestContext>
    {
        protected override ITransportProvider CreateTransportProvider(IContextCorrelator contextCorrelator)
        {
            var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Host.CreateHandler);
            return SignalRTransportProvider.Create(httpMessageHandlerFactory);
        }

        protected override IStorageTransportProvider CreateStorageTransportProvider(IContextCorrelator contextCorrelator)
        {
            var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Host.CreateHandler);
            return SignalRStorageTransportProvider.Create(httpMessageHandlerFactory);
        }
    }
}
