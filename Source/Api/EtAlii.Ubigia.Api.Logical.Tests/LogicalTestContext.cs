// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Fabric.Diagnostics;
using EtAlii.Ubigia.Api.Fabric.Tests;
using EtAlii.xTechnology.Hosting;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using Microsoft.Extensions.Configuration;

public class LogicalTestContext : ILogicalTestContext
{
    public IFabricTestContext Fabric { get; }

    public IConfigurationRoot ClientConfiguration => Fabric.Transport.Host.ClientConfiguration;
    public IConfigurationRoot HostConfiguration => Fabric.Transport.Host.HostConfiguration;

    public LogicalTestContext(IFabricTestContext fabric)
    {
        Fabric = fabric;
    }

    public LogicalOptions CreateLogicalOptionsWithoutConnection()
    {
        return new FabricOptions(ClientConfiguration)
            .UseDiagnostics()
            .UseLogicalContext()
            .UseDiagnostics();
    }

    public async Task<LogicalOptions> CreateLogicalOptionsWithConnection(bool openOnCreation)
    {
        var logicalOptions = await new FabricOptions(ClientConfiguration)
            .UseDiagnostics()
            .UseDataConnectionToNewSpace(this, openOnCreation)
            .UseLogicalContext()
            .UseDiagnostics()
            .ConfigureAwait(false);

        return logicalOptions;
    }

    public async Task<LocationAddResult> AddContinentCountry(ILogicalContext logicalContext)
    {
        var scope = new ExecutionScope();
        // Root.
        // Location.
        // [LINK]
        // yyyy
        // [LINK]
        // mm

        var locationRoot = await logicalContext.Roots.Get("Location").ConfigureAwait(false);
        var continent = "Europe";
        var country = "NL";

        var continentEntry = await logicalContext.Nodes.Add(locationRoot.Identifier, continent, scope).ConfigureAwait(false);
        var countryEntry = (IEditableEntry)await logicalContext.Nodes.Add(continentEntry.Id, country, scope).ConfigureAwait(false);
        var path = $"/Location/{continent}/{country}";
        return new LocationAddResult(path, countryEntry);
    }

    public async Task<string> AddContinentCountryRegionCityLocation(ILogicalContext logicalContext)
    {
        var locationRoot = await logicalContext.Roots.Get("Location").ConfigureAwait(false);
        var continent = "Europe";
        var country = "NL";
        var region = "Overijssel";
        var city = "Enschede";
        var location = "Helmerhoek";

        var scope = new ExecutionScope();

        var continentEntry = await logicalContext.Nodes.Add(locationRoot.Identifier, continent, scope).ConfigureAwait(false);
        var countryEntry = (IEditableEntry)await logicalContext.Nodes.Add(continentEntry.Id, country, scope).ConfigureAwait(false);
        var regionEntry = (IEditableEntry)await logicalContext.Nodes.Add(countryEntry.Id, region, scope).ConfigureAwait(false);
        var cityEntry = (IEditableEntry)await logicalContext.Nodes.Add(regionEntry.Id, city, scope).ConfigureAwait(false);
        await logicalContext.Nodes.Add(cityEntry.Id, location, scope).ConfigureAwait(false);
        return $"/Location/{continent}/{country}/{region}/{city}/{location}";
    }

    public async Task AddRegions(ILogicalContext logicalContext, IEditableEntry countryEntry, int regions)
    {
        var scope = new ExecutionScope();

        for (var i = 1; i <= regions; i++)
        {
            await CreateHierarchy(logicalContext, countryEntry, scope, $"Overijssel_{i:00}").ConfigureAwait(false);
        }
    }

    public async Task<IEditableEntry> CreateHierarchy(ILogicalContext logicalContext, IEditableEntry parent, params string[] hierarchy)
    {
        var scope = new ExecutionScope();

        return await CreateHierarchy(logicalContext, parent, scope, hierarchy).ConfigureAwait(false);
    }

    private async Task<IEditableEntry> CreateHierarchy(ILogicalContext logicalContext, IEditableEntry parent, ExecutionScope scope, params string[] hierarchy)
    {
        var result = parent;
        foreach (var element in hierarchy)
        {
            result = (IEditableEntry)await logicalContext.Nodes
                .Add(result.Id, element, scope)
                .ConfigureAwait(false);
        }
        return result;
    }

    public async Task Start(PortRange portRange)
    {
        await Fabric
            .Start(portRange)
            .ConfigureAwait(false);
    }

    public async Task Stop()
    {
        await Fabric
            .Stop()
            .ConfigureAwait(false);
    }
}
