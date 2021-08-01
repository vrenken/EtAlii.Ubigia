﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Reflection;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class ProfilingLogicalContextTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public ProfilingLogicalContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_01()
        {
            // Arrange.
            var clientConfiguration = await GetProfilingClientConfiguration().ConfigureAwait(false);

            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(clientConfiguration);
            await _testContext.Fabric
                .ConfigureFabricContextConfiguration(configuration, true)
                .ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration, clientConfiguration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_02()
        {
            // Arrange.
            var clientConfiguration = await GetProfilingClientConfiguration().ConfigureAwait(false);

            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(clientConfiguration);
            await _testContext.Fabric
                .ConfigureFabricContextConfiguration(configuration, true)
                .ConfigureAwait(false);


            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration, clientConfiguration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_03()
        {
            // Arrange.
            var clientConfiguration = await GetProfilingClientConfiguration().ConfigureAwait(false);

            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(clientConfiguration);
            await _testContext.Fabric
                .ConfigureFabricContextConfiguration(configuration, true)
                .ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration, clientConfiguration);

            // Assert.
            Assert.NotNull(context);
        }


        private async Task<IConfigurationRoot> GetProfilingClientConfiguration()
        {
            var type = typeof(LogicalUnitTestContext);
            // Get the current executing assembly (in this case it's the test dll)
            var assembly = Assembly.GetAssembly(type);
            // Get the stream (embedded resource) - be sure to wrap in a using block
            await using var stream = assembly!.GetManifestResourceStream($"{type.Namespace}.LogicalProfilingSettings.json");

            var clientConfiguration = new ConfigurationBuilder()
                .AddConfiguration(_testContext.ClientConfiguration)
                .AddJsonStream(stream)
                .Build();
            return clientConfiguration;
        }
    }
}
