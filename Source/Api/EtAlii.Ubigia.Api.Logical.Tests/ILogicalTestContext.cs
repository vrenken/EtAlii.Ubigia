// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public interface ILogicalTestContext
    {
        IFabricTestContext Fabric { get; }

        IConfigurationRoot ClientConfiguration { get; }
        IConfigurationRoot HostConfiguration { get; }

        LogicalOptions CreateLogicalOptionsWithoutConnection();

        Task<LogicalOptions> CreateLogicalOptionsWithConnection(bool openOnCreation);

        Task<IEditableEntry> CreateHierarchy(ILogicalContext logicalContext, IEditableEntry parent, params string[] hierarchy);

        Task<string> AddContinentCountryRegionCityLocation(ILogicalContext logicalContext);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
