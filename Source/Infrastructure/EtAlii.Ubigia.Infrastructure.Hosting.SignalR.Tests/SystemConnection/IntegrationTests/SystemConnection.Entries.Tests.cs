// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class SystemConnectionEntriesTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public SystemConnectionEntriesTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SystemConnection_Entry_Prepare()
        {
            // Arrange.
            var userName = Guid.NewGuid().ToString();// "TestUser"
            var password = "123";
            var spaceName = "TestSpace";
            var (systemConnection, _) = await _testContext.Host
                .CreateSystemConnection()
                .AddUserAccountAndSpaces(userName, password, new[] { spaceName })
                .ConfigureAwait(false);

            var (connection, _) = await systemConnection
                .OpenSpace(userName, spaceName)
                .ConfigureAwait(false);

            // Act.
            var entry = await connection.Entries.Data
                .Prepare()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(entry);
        }
    }
}
