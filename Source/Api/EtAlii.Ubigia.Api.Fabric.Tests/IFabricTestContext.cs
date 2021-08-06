// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public interface IFabricTestContext
    {
        ITransportTestContext Transport { get; }

        public IConfigurationRoot ClientConfiguration { get; }
        public IConfigurationRoot HostConfiguration { get; }

        Task ConfigureFabricContextOptions(FabricContextOptions fabricContextOptions, bool openOnCreation);
        Task<IFabricContext> CreateFabricContext(bool openOnCreation);
        Task<Tuple<IEditableEntry, string[]>> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, int depth);//, out string[] hierarchy)

        Task Start(PortRange portRange);
        Task Stop();
    }
}
