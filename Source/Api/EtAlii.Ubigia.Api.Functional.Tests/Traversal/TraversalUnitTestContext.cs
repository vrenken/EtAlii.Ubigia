// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using UnitTestSettings = EtAlii.Ubigia.Api.Functional.Tests.UnitTestSettings;
    using Microsoft.Extensions.Configuration;

    public class TraversalUnitTestContext : IAsyncLifetime
    {
        public ILogicalTestContext Logical { get; private set; }

        public IConfiguration ClientConfiguration => Logical.Fabric.Transport.Host.ClientConfiguration;
        public IConfiguration HostConfiguration => Logical.Fabric.Transport.Host.HostConfiguration;

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }

        public TraversalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);
        }

        public async Task InitializeAsync()
        {
            Logical = new LogicalTestContextFactory().Create();
            await Logical.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Logical.Stop().ConfigureAwait(false);
            Logical = null;
        }

        public IScriptProcessor CreateScriptProcessor(ILogicalContext logicalContext, ScriptScope scope = null)
        {
            scope ??= new ScriptScope();
            var options = new TraversalProcessorOptions()
                .UseFunctionalDiagnostics(ClientConfiguration)
                .UseTestProcessor()
                .Use(logicalContext)
                .Use(scope);
            return new ScriptProcessorFactory().Create(options);
        }
    }
}
