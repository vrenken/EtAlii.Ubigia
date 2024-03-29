// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.Ubigia.Api.Logical.Tests;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;
using Xunit;

public class FunctionalUnitTestContext : IAsyncLifetime
{
    public IFunctionalTestContext Functional { get; private set; }

    public ILogicalTestContext Logical => Functional.Logical;

    public FileComparer FileComparer { get; }
    public FolderComparer FolderComparer { get; }

    public IConfigurationRoot ClientConfiguration => Functional.Logical.Fabric.Transport.Host.ClientConfiguration;
    public IConfigurationRoot HostConfiguration => Functional.Logical.Fabric.Transport.Host.HostConfiguration;

    public FunctionalUnitTestContext()
    {
        FileComparer = new FileComparer();
        FolderComparer = new FolderComparer(FileComparer);
    }

    public async Task InitializeAsync()
    {
        var logicalTestContext = new LogicalTestContextFactory().Create();
        Functional = new FunctionalTestContext(logicalTestContext);
        await Functional
            .Start(UnitTestSettings.NetworkPortRange)
            .ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await Functional.Stop().ConfigureAwait(false);
        Functional = null;
    }

    public async Task<Root> GetRoot(ILogicalContext logicalContext, string rootName)
    {
        return await logicalContext.Roots
            .Get(rootName)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyEntry> GetEntry(ILogicalContext logicalContext, Identifier identifier, ExecutionScope scope)
    {
        return await logicalContext.Nodes
            .SelectSingle(GraphPath.Create(identifier), scope)
            .ConfigureAwait(false);
    }

    public TInstance CreateComponent<TInstance>(FunctionalOptions options) => Factory.Create<TInstance>(options);

    public (TFirstInstance, TSecondInstance) CreateComponent<TFirstInstance, TSecondInstance>(FunctionalOptions options) => Factory.Create<TFirstInstance, TSecondInstance>(options);

    public ISchemaProcessor CreateSchemaProcessor(FunctionalOptions options) => Factory.Create<ISchemaProcessor>(options);
}
