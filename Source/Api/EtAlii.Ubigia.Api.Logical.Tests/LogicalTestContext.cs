// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

    public class LogicalTestContext : ILogicalTestContext
    {
        public IFabricTestContext Fabric { get; }

        public IConfiguration ClientConfiguration => Fabric.Transport.Host.ClientConfiguration;
        public IConfiguration HostConfiguration => Fabric.Transport.Host.HostConfiguration;

        public LogicalTestContext(IFabricTestContext fabric)
        {
            Fabric = fabric;
        }

        public async Task ConfigureLogicalContextConfiguration(LogicalContextConfiguration configuration, bool openOnCreation)
        {
            await Fabric.ConfigureFabricContextConfiguration(configuration, openOnCreation).ConfigureAwait(false);
        }

        public async Task<ILogicalContext> CreateLogicalContext(bool openOnCreation)
        {
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(Fabric.ClientConfiguration);
            await Fabric.ConfigureFabricContextConfiguration(configuration, openOnCreation).ConfigureAwait(false);
            return new LogicalContextFactory().Create(configuration);
        }

        public async Task<LocationAddResult> AddContinentCountry(ILogicalContext context)
        {
            var scope = new ExecutionScope(false);
            // Root.
            // Location.
            // [LINK]
            // yyyy
            // [LINK]
            // mm

            var locationRoot = await context.Roots.Get("Location").ConfigureAwait(false);
            var continent = "Europe";
            var country = "NL";

            var continentEntry = await context.Nodes.Add(locationRoot.Identifier, continent, scope).ConfigureAwait(false);
            var countryEntry = (IEditableEntry)await context.Nodes.Add(continentEntry.Id, country, scope).ConfigureAwait(false);
            var path = $"/Location/{continent}/{country}";
            return new LocationAddResult(path, countryEntry);
        }

        public async Task<string> AddContinentCountryRegionCityLocation(ILogicalContext context)
        {
            var locationRoot = await context.Roots.Get("Location").ConfigureAwait(false);
            var continent = "Europe";
            var country = "NL";
            var region = "Overijssel";
            var city = "Enschede";
            var location = "Helmerhoek";

            var scope = new ExecutionScope(false);

            var continentEntry = await context.Nodes.Add(locationRoot.Identifier, continent, scope).ConfigureAwait(false);
            var countryEntry = (IEditableEntry)await context.Nodes.Add(continentEntry.Id, country, scope).ConfigureAwait(false);
            var regionEntry = (IEditableEntry)await context.Nodes.Add(countryEntry.Id, region, scope).ConfigureAwait(false);
            var cityEntry = (IEditableEntry)await context.Nodes.Add(regionEntry.Id, city, scope).ConfigureAwait(false);
            await context.Nodes.Add(cityEntry.Id, location, scope).ConfigureAwait(false);
            return $"/Location/{continent}/{country}/{region}/{city}/{location}";
        }

        public async Task AddRegions(ILogicalContext context, IEditableEntry countryEntry, int regions)
        {
            for (var i = 1; i <= regions; i++)
            {
                await CreateHierarchy(context, countryEntry, $"Overijssel_{i:00}").ConfigureAwait(false);
            }
        }

        public async Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy)
        {
            var scope = new ExecutionScope(false);

            var result = parent;
            foreach (var element in hierarchy)
            {
                result = (IEditableEntry)await context.Nodes.Add(result.Id, element, scope).ConfigureAwait(false);
            }
            return result;
        }


        #region start/stop

        public async Task Start(PortRange portRange)
        {
            await Fabric.Start(portRange).ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Fabric.Stop().ConfigureAwait(false);
        }

        #endregion start/stop
    }
}
