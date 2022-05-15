// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;
    using Xunit;

    [CorrelateUnitTests]
    public class SignalRSpaceClientsScaffoldingTests  : IClassFixture<TransportUnitTestContext>
    {
        private readonly TransportUnitTestContext _testContext;

        public SignalRSpaceClientsScaffoldingTests(TransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void SignalRSpaceClientsScaffolding_Create()
        {
            // Arrange.
            var configurationRoot = _testContext.TransportTestContext.Host.ClientConfiguration;
            var options = new SpaceConnectionOptions(configurationRoot);

            // Act.
            var scaffolding = new SignalRSpaceClientsScaffolding(options);

            // Assert.
            Assert.NotNull(scaffolding);
        }

        [Fact]
        public void SignalRSpaceClientsScaffolding_Register()
        {
            // Arrange.
            var configurationRoot = _testContext.TransportTestContext.Host.ClientConfiguration;
            var options = new SpaceConnectionOptions(configurationRoot);
            var scaffolding = new SignalRSpaceClientsScaffolding(options);
            var container = new Container();

            // Act.
            scaffolding.Register(container);

            // Assert.
            Assert.NotNull(scaffolding);
        }

        [Fact]
        public void SignalRSpaceClientsScaffolding_Get_Connection()
        {
            // Arrange.
            var configurationRoot = _testContext.TransportTestContext.Host.ClientConfiguration;
            var options = new SpaceConnectionOptions(configurationRoot)
                .Use(new SignalRSpaceTransport(new Uri("https://nowhere"), () => null, s => { }, () => string.Empty));
            var scaffolding = new SignalRSpaceClientsScaffolding(options);
            var container = new Container();

            container.Register<IContextCorrelator, ContextCorrelator>();
            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IEntryContext, EntryContext>();
            container.Register<IRootContext, RootContext>();
            container.Register<IContentContext, ContentContext>();
            container.Register<IPropertiesContext, PropertiesContext>();

            container.Register(() => options.Transport);

            // Act.
            scaffolding.Register(container);

            // Assert.
            var connection = container.GetInstance<ISpaceConnection>();
            Assert.NotNull(connection);
        }
    }
}
