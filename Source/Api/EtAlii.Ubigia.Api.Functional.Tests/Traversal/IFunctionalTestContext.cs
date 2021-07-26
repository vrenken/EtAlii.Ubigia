// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Hosting;

    public interface IFunctionalTestContext
    {
        Task ConfigureLogicalContextConfiguration(LogicalContextConfiguration configuration, bool openOnCreation);

        //Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);

        Task AddPeople(ITraversalContext context);
        Task AddAddresses(ITraversalContext context);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
