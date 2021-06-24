// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public interface IFabricTestContext
    {
        Task ConfigureFabricContextConfiguration(FabricContextConfiguration fabricContextConfiguration, bool openOnCreation);
        Task<IFabricContext> CreateFabricContext(bool openOnCreation);
        Task<Tuple<IEditableEntry, string[]>> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, int depth);//, out string[] hierarchy)

        Task Start(PortRange portRange);
        Task Stop();
    }
}
