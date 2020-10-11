namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public interface ILogicalTestContext
    {

        Task ConfigureLogicalContextConfiguration(LogicalContextConfiguration configuration, bool openOnCreation);
        
        Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);
        Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy);

        Task<LocationAddResult> AddContinentCountry(ILogicalContext context);
        Task<string> AddContinentCountryRegionCityLocation(ILogicalContext logicalContext);
        Task AddRegions(ILogicalContext context, IEditableEntry countryEntry, int regions);

        Task Start(PortRange portRange);
        Task Stop();
    }
}