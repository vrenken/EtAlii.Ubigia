namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;

    public class LogicalTestContext : ILogicalTestContext
    {
        private readonly IFabricTestContext _fabric;

        public LogicalTestContext(IFabricTestContext fabric)
        {
            _fabric = fabric;
        }

        public async Task ConfigureLogicalContextConfiguration(LogicalContextConfiguration configuration, bool openOnCreation)
        {
            await _fabric.ConfigureFabricContextConfiguration(configuration, openOnCreation);
        }
        
        public async Task<ILogicalContext> CreateLogicalContext(bool openOnCreation)
        {
            var configuration = new LogicalContextConfiguration();
            await _fabric.ConfigureFabricContextConfiguration(configuration, openOnCreation);
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

            var locationRoot = await context.Roots.Get("Location");
            var continent = "Europe";
            var country = "NL";

            var continentEntry = await context.Nodes.Add(locationRoot.Identifier, continent, scope);
            var countryEntry = (IEditableEntry)await context.Nodes.Add(continentEntry.Id, country, scope);
            var path = $"/Location/{continent}/{country}";
            return new LocationAddResult(path, countryEntry);
        }
        
        public async Task<string> AddContinentCountryRegionCityLocation(ILogicalContext context)
        {
            var locationRoot = await context.Roots.Get("Location");
            string continent = "Europe";
            string country = "NL";
            string region = "Overijssel";
            string city = "Enschede";
            string location = "Helmerhoek";

            var scope = new ExecutionScope(false);

            var continentEntry = await context.Nodes.Add(locationRoot.Identifier, continent, scope);
            var countryEntry = (IEditableEntry)await context.Nodes.Add(continentEntry.Id, country, scope);
            var regionEntry = (IEditableEntry)await context.Nodes.Add(countryEntry.Id, region, scope);
            var cityEntry = (IEditableEntry)await context.Nodes.Add(regionEntry.Id, city, scope);
            var locationEntry = (IEditableEntry)await context.Nodes.Add(cityEntry.Id, location, scope);
            return $"/Location/{continent}/{country}/{region}/{city}/{location}";
        }

        public async Task AddRegions(ILogicalContext context, IEditableEntry countryEntry, int regions)
        {
            for (int i = 1; i <= regions; i++)
            {
                await CreateHierarchy(context, countryEntry, $"Overijssel_{i:00}");
            }
        }

        public async Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy)
        {
            var scope = new ExecutionScope(false);

            IEditableEntry result = parent;
            foreach (var element in hierarchy)
            {
                result = (IEditableEntry)await context.Nodes.Add(result.Id, element, scope);
            }
            return result;
        }


        #region start/stop

        public async Task Start()
        {
            await _fabric.Start();
        }

        public async Task Stop()
        {
            await _fabric.Stop();
        }

        #endregion start/stop
    }
}