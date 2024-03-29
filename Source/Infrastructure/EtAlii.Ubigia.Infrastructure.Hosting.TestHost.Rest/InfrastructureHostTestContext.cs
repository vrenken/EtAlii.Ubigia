﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

using System.Linq;
using System.Threading.Tasks;
using EtAlii.xTechnology.Hosting;
using EtAlii.Ubigia.Api.Transport.Rest;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest;
using EtAlii.xTechnology.Threading;

/// <summary>
/// We need to make the name of this HostTestContext transport-agnostic in order for it to be used in all
/// unit tests. Reason is that these are reused using shared projects.
/// </summary>
public class InfrastructureHostTestContext : HostTestContextBase, IInfrastructureHostTestContext
{
    private readonly IContextCorrelator _contextCorrelator = new ContextCorrelator();

    /// <inheritdoc />
    public override async Task Start(PortRange portRange)
    {
        await base
            .Start(portRange)
            .ConfigureAwait(false);
        ServiceDetails = Functional.Options.ServiceDetails.Single(sd => sd.Name == ServiceDetailsName.Rest);
    }

    public IRestInfrastructureClient CreateRestInfrastructureClient()
    {
        var httpClientFactory = new TestHttpClientFactory(this, _contextCorrelator);
        var infrastructureClient = new RestInfrastructureClient(httpClientFactory);
        return infrastructureClient;
    }
}
