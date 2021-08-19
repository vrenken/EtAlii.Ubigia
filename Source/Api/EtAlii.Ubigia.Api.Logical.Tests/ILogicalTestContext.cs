// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public interface ILogicalTestContext
    {
        IFabricTestContext Fabric { get; }

        IConfigurationRoot ClientConfiguration { get; }
        IConfigurationRoot HostConfiguration { get; }

        ILogicalContext CreateLogicalContextWithoutConnection();
        Task<ILogicalContext> CreateLogicalContextWithConnection(bool openOnCreation);
        ILogicalContext CreateLogicalContextWithConnection(IDataConnection dataConnection);

        Task<IEditableEntry> CreateHierarchy(ILogicalContext context, IEditableEntry parent, params string[] hierarchy);

        Task<LocationAddResult> AddContinentCountry(ILogicalContext context);
        Task<string> AddContinentCountryRegionCityLocation(ILogicalContext logicalContext);
        Task AddRegions(ILogicalContext context, IEditableEntry countryEntry, int regions);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
