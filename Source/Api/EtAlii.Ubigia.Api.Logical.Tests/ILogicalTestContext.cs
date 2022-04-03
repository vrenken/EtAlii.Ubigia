// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using Microsoft.Extensions.Configuration;

    public interface ILogicalTestContext
    {
        IFabricTestContext Fabric { get; }

        IConfigurationRoot ClientConfiguration { get; }
        IConfigurationRoot HostConfiguration { get; }

        LogicalOptions CreateLogicalOptionsWithoutConnection();

        Task<LogicalOptions> CreateLogicalOptionsWithConnection(bool openOnCreation);

        Task<IEditableEntry> CreateHierarchy(LogicalOptions logicalOptions, IEditableEntry parent, params string[] hierarchy);

        Task<string> AddContinentCountryRegionCityLocation(LogicalOptions logicalOptions);

        Task Start();
        Task Stop();
    }
}
