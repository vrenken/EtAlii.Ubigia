// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class RemoteGrpcHostTests
    {
        [Fact(Skip = "This test should not be run on the server, but can be used locally to see if the console/docker applications facilitate Grpc requests.")]
        public async Task RemoteGrpcHost_Connect_Using_Admin_Transport()
        {
            // Arrange.
            var correlator = new ContextCorrelator();
            using var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider>());
            var connectionOptions = new ManagementConnectionOptions(configurationRoot)
                .Use(GrpcStorageTransportProvider.Create(correlator))
                .Use("Administrator", "administrator123")
                .Use(new Uri("https://localhost:64001"));
            using var managementConnection = Factory.Create<IManagementConnection>(connectionOptions);
            await managementConnection
                .Open()
                .ConfigureAwait(false);

            // Act.
            var accounts = await managementConnection.Accounts
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Equal(2, accounts.Length);
        }
    }
}
