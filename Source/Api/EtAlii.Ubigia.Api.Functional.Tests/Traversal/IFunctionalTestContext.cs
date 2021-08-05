// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public interface IFunctionalTestContext
    {
        ILogicalTestContext Logical { get; }

        public IConfiguration ClientConfiguration { get; }
        public IConfiguration HostConfiguration { get; }

        Task ConfigureLogicalContextOptions(LogicalContextOptions options, bool openOnCreation);

        Task AddPeople(ITraversalContext context);
        Task AddAddresses(ITraversalContext context);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
