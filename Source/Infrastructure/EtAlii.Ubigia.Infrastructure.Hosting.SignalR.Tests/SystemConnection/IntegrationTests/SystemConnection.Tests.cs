﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;

    [CorrelateUnitTests]
    public class SystemConnectionTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public SystemConnectionTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SystemConnection_Create()
        {
            // Arrange.

            // Act.
            var (connection, _) = await _testContext.Host
                .CreateSystemConnection()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
        }

        [Fact]
        public async Task SystemConnection_Create_DataConnection()
        {
            // Arrange.
            var userName = "TestUser";
            var password = "123";
            var spaceName = "TestSpace";
            var (systemConnection, _) = await _testContext.Host
                .CreateSystemConnection()
                .AddUserAccountAndSpaces(userName, password, new[] { spaceName })
                .ConfigureAwait(false);

            // Act.
            var (connection, _) = await systemConnection
                .OpenSpace("TestUser", "TestSpace")
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
            Assert.NotNull(connection.Storage);
            Assert.NotNull(connection.Account);
            Assert.Equal(userName, connection.Account.Name);
            Assert.NotNull(connection.Space);
            Assert.Equal(spaceName, connection.Space.Name);
        }

        [Fact]
        public async Task SystemConnection_Create_ManagementConnection()
        {
            // Arrange.
            var userName = Guid.NewGuid().ToString();// "TestUser"
            var password = "123";
            var spaceName = "TestSpace";
            var (systemConnection, _) = await _testContext.Host
                .CreateSystemConnection()
                .AddUserAccountAndSpaces(userName, password, new[] { spaceName })
                .ConfigureAwait(false);

            // Act.
            var connection = await systemConnection
                .OpenManagementConnection()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(connection);
            Assert.NotNull(connection.Storage);
            var account = await connection.Accounts
                .Get(userName)
                .ConfigureAwait(false);
            Assert.NotNull(account);
            Assert.Equal(userName, account.Name);
            var space = await connection.Spaces
                .Get(account.Id, spaceName)
                .ConfigureAwait(false);
            Assert.NotNull(space);
            Assert.Equal(spaceName, space.Name);
        }

        [Fact]
        public async Task SystemConnection_Advanced_Operation_Single_Space_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            var functionalOptions = await _testContext.Host
                .CreateSystemConnection() // Transport.
                .AddUserAccountAndSpaces(accountName, password, new[] { spaceName })
                .OpenSpace(accountName, spaceName)
                .UseFabricContext() // Fabric.
                .UseDiagnostics()
                .UseLogicalContext() // Logical.
                .UseDiagnostics()
                .UseFunctionalContext() // Functional.
                .UseTestParsing()
                .UseDiagnostics()
                .ConfigureAwait(false);

            var context = Factory.Create<ITraversalContext>(functionalOptions);

            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "<= Count() <= /Person/Doe/*";

            var addScript = context.Parse(addQuery, scope).Script;
            var selectScript = context.Parse(selectQuery, scope).Script;
            scope = new ExecutionScope();

            // Act.
            var lastSequence = await context.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await context.Process(selectScript, scope);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsAfter);
            Assert.Single(personsAfter);
            Assert.Equal(3, personsAfter.Single());
        }

        [Fact]
        public async Task SystemConnection_Advanced_Operation_Single_Space_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            var functionalOptions = await _testContext.Host
                .CreateSystemConnection() // Transport.
                .AddUserAccountAndSpaces(accountName, password, new[] { spaceName })
                .OpenSpace(accountName, spaceName)
                .UseFabricContext() // Fabric.
                .UseDiagnostics()
                .UseLogicalContext() // Logical.
                .UseDiagnostics()
                .UseFunctionalContext() // Functional.
                .UseTestParsing()
                .UseDiagnostics()
                .ConfigureAwait(false);

            var context = Factory.Create<ITraversalContext>(functionalOptions);

            var selectQuery = "<= /Person";

            var selectScript = context.Parse(selectQuery, scope).Script;
            scope = new ExecutionScope();

            // Act.
            var lastSequence = await context.Process(selectScript, scope);
            var item = await lastSequence.Output.ToArray();

            // Assert.
            Assert.Single(item);
            Assert.IsAssignableFrom<Node>(item[0]);
        }
    }
}
