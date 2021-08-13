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

        public IConfigurationRoot ClientConfiguration => Logical.ClientConfiguration;
        public IConfigurationRoot HostConfiguration => Logical.HostConfiguration;

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
            await Logical
                .Start(UnitTestSettings.NetworkPortRange)
                .ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Logical
                .Stop()
                .ConfigureAwait(false);
            Logical = null;
        }
        //
        // public async Task<FunctionalOptions> CreateFunctionalOptions(bool openConnectionOnCreation = true)
        // {
        //     var logicalContext = await Logical
        //         .CreateLogicalContext(openConnectionOnCreation)
        //         .ConfigureAwait(false);
        //
        //     var options = new FunctionalOptions(ClientConfiguration)
        //         .Use(logicalContext.Options.Connection)
        //         .UseTestParser();
        //
        //     return options;
        // }
        //
        public IScriptProcessor CreateScriptProcessor(ILogicalContext logicalContext, FunctionalScope scope = null)
        {
            scope ??= new FunctionalScope();
            var options = new FunctionalOptions(ClientConfiguration)
                .UseTestProcessor()
                .Use(logicalContext.Options.Connection)
                .Use(scope)
                .UseFunctionalDiagnostics();
            return new ScriptProcessorFactory().Create(options);
        }
    }
}
