namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;

    public interface ILogicalTestContext
    {
        Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);
        Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy);

        Task<LocationAddResult> AddContinentCountry(ILogicalContext context);
        Task<string> AddContinentCountryRegionCityLocation(ILogicalContext logicalContext);
        Task AddRegions(ILogicalContext context, IEditableEntry countryEntry, int regions);

        Task Start();
        Task Stop();
    }
}