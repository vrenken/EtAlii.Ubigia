// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests;

using System;
using EtAlii.Ubigia.Api.Transport.Tests;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;
using EtAlii.xTechnology.Threading;
using Xunit;

[CorrelateUnitTests]
public class RestSpaceClientsScaffoldingTests  : IClassFixture<TransportUnitTestContext>
{
    private readonly TransportUnitTestContext _testContext;

    public RestSpaceClientsScaffoldingTests(TransportUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public void RestSpaceClientsScaffolding_Create()
    {
        // Arrange.
        var configurationRoot = _testContext.TransportTestContext.Host.ClientConfiguration;
        var options = new SpaceConnectionOptions(configurationRoot);
        var contextCorrelator = new ContextCorrelator();
        var clientFactory = new RestHttpClientFactory(contextCorrelator);
        var infrastructureClient = new RestInfrastructureClient(clientFactory);

        // Act.
        var scaffolding = new RestSpaceClientsScaffolding(infrastructureClient, options);

        // Assert.
        Assert.NotNull(scaffolding);
    }

    [Fact]
    public void RestSpaceClientsScaffolding_Register()
    {
        // Arrange.
        var configurationRoot = _testContext.TransportTestContext.Host.ClientConfiguration;
        var options = new SpaceConnectionOptions(configurationRoot);
        var contextCorrelator = new ContextCorrelator();
        var clientFactory = new RestHttpClientFactory(contextCorrelator);
        var infrastructureClient = new RestInfrastructureClient(clientFactory);
        var scaffolding = new RestSpaceClientsScaffolding(infrastructureClient, options);
        var container = new Container();

        // Act.
        scaffolding.Register(container);

        // Assert.
        Assert.NotNull(scaffolding);
    }

    [Fact]
    public void RestSpaceClientsScaffolding_Get_Connection()
    {
        // Arrange.
        var contextCorrelator = new ContextCorrelator();
        var clientFactory = new RestHttpClientFactory(contextCorrelator);
        var infrastructureClient = new RestInfrastructureClient(clientFactory);
        var configurationRoot = _testContext.TransportTestContext.Host.ClientConfiguration;
        var options = new SpaceConnectionOptions(configurationRoot)
            .Use(new RestSpaceTransport(new Uri("https://nowhere"), infrastructureClient));
        var scaffolding = new RestSpaceClientsScaffolding(infrastructureClient, options);
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
