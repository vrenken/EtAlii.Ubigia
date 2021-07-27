// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.xTechnology.Hosting;
    using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

    public interface ILogicalTestContext
    {
        IFabricTestContext Fabric { get; }

        IConfiguration ClientConfiguration { get; }
        IConfiguration HostConfiguration { get; }

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
