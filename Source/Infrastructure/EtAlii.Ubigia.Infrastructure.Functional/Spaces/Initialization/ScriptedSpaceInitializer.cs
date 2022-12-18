// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Fabric.Diagnostics;
using EtAlii.Ubigia.Api.Functional;
using EtAlii.Ubigia.Api.Functional.Antlr;
using EtAlii.Ubigia.Api.Functional.Traversal;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;
using ILogicalContext = EtAlii.Ubigia.Infrastructure.Logical.ILogicalContext;

internal class ScriptedSpaceInitializer : ISpaceInitializer
{
    private readonly ILogicalContext _context;

    private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;
    private readonly IConfigurationRoot _configurationRoot;

    public ScriptedSpaceInitializer(
        ILogicalContext context,
        ISystemConnectionCreationProxy systemConnectionCreationProxy,
        IConfigurationRoot configurationRoot)
    {
        _context = context;
        _systemConnectionCreationProxy = systemConnectionCreationProxy;
        _configurationRoot = configurationRoot;
    }

    public async Task Initialize(Space space, SpaceTemplate template)
    {
        var storage = await _context.Storages
            .GetLocal()
            .ConfigureAwait(false);
        var storageId = storage.Id;
        var accountId = space.AccountId;
        var spaceId = space.Id;

        var hasRoots = await _context.Roots
            .GetAll(spaceId)
            .AnyAsync()
            .ConfigureAwait(false);

        if (hasRoots)
        {
            throw new InvalidOperationException("The space already contains roots");
        }

        // Transport.
        var systemConnection = _systemConnectionCreationProxy.Request();
        var managementConnection = await systemConnection
            .OpenManagementConnection()
            .ConfigureAwait(false);
        var spaceConnection = await managementConnection
            .OpenSpace(space)
            .ConfigureAwait(false);

        // Fabric.
        var functionalOptions = new FabricOptions(_configurationRoot)
            .UseCaching(true)
            .Use(spaceConnection)
            .UseDiagnostics()
            .UseLogicalContext() // Logical.
            .UseDiagnostics()
            .UseFunctionalContext() // Functional.
            .UseAntlrParsing()
            .UseDiagnostics();
        var scriptContext = Factory.Create<ITraversalContext>(functionalOptions);

        var rootsToCreate = template.RootsToCreate;

        var scope = new ExecutionScope();
        foreach (var rootToCreate in rootsToCreate)
        {
            var createScript = scriptContext.Parse($"root:{rootToCreate.Name} <= {rootToCreate.Type}", scope);
            var processingResult = await scriptContext.Process(createScript.Script, scope);
            await processingResult.Output.LastOrDefaultAsync();
        }
    }
}
