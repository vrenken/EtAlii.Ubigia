// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public interface IFunctionalTestContext
    {
        ILogicalTestContext Logical { get; }

        public IConfigurationRoot ClientConfiguration { get; }
        public IConfigurationRoot HostConfiguration { get; }

        Task AddPeople(ITraversalContext context, ExecutionScope scope);
        Task AddAddresses(ITraversalContext context, ExecutionScope scope);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
