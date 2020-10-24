﻿namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ManagementConnectionSpacesDataTests : IClassFixture<StartedTransportUnitTestContext>, IDisposable
    {
        private readonly StartedTransportUnitTestContext _testContext;

        public ManagementConnectionSpacesDataTests(StartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public void Dispose()
        {
            // Dispose any relevant resources.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Add_Single_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);
            var name = Guid.NewGuid().ToString();

            // Act.
            var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);

            // Assert.
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Add_Multiple_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                // Act.
                var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);

                // Assert.
                Assert.NotNull(space);
                Assert.Equal(name, space.Name);
                Assert.Equal(account.Id, space.AccountId);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Get_Single_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);
            var name = Guid.NewGuid().ToString();
            var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);

            // Act.
            space = await connection.Spaces.Get(space.Id);

            // Assert.
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Get_Multiple_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);

                // Act.
                space = await connection.Spaces.Get(space.Id);

                // Assert.
                Assert.NotNull(space);
                Assert.Equal(name, space.Name);
                Assert.Equal(account.Id, space.AccountId);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Get_First_Data_Full_Add()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);

            var spaces = new List<Space>();

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                // Act.
                var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);
                
                // Assert.
                Assert.NotNull(space);
                Assert.Equal(name, space.Name);
                Assert.Equal(account.Id, space.AccountId);
                spaces.Add(space);
            }

            foreach (var space in spaces)
            {
                var retrievedSpace = await connection.Spaces.Get(space.Id);

                Assert.NotNull(retrievedSpace);
                Assert.Equal(space.Name, retrievedSpace.Name);
                Assert.Equal(account.Id, retrievedSpace.AccountId);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Get_No_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);

            // Act.
            var retrievedSpaces = await connection.Spaces
                .GetAll(account.Id)
                .ToArrayAsync();
            
            // Assert.
            Assert.NotNull(retrievedSpaces);
            // Each user is initialized with at least a configuration and a data space. so we need to expect two spaces .
            Assert.Equal(2, retrievedSpaces.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Get_All_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);

            var spaces = new List<Space>();

            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);
                Assert.NotNull(space);
                Assert.Equal(name, space.Name);
                Assert.Equal(account.Id, space.AccountId);
                spaces.Add(space);
            }

            // Act.
            var retrievedSpaces = await connection.Spaces
                .GetAll(account.Id)
                .ToArrayAsync();

            // Assert.
            // Each user is initialized with at least a configuration and a data space. so we need to add two to the amount of spaces we expect.
            Assert.Equal(spaces.Count + 2, retrievedSpaces.Count());

            foreach (var space in spaces)
            {
                var matchingSpace = retrievedSpaces.Single(s => s.Id == space.Id);
                Assert.NotNull(matchingSpace);
                Assert.Equal(space.Name, matchingSpace.Name);
                Assert.Equal(account.Id, matchingSpace.AccountId);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Change_Data()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);
            var name = Guid.NewGuid().ToString();

            // Act.
            var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);

            // Assert.
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);

            space = await connection.Spaces.Get(space.Id);
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);

            name = Guid.NewGuid().ToString();

            space = await connection.Spaces.Change(space.Id, name);
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);

            space = await connection.Spaces.Get(space.Id);
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Delete()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);

            var name = Guid.NewGuid().ToString();

            // Act.
            var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);
            
            // Assert.
            Assert.NotNull(space);
            Assert.Equal(name, space.Name);
            Assert.Equal(account.Id, space.AccountId);

            space = await connection.Spaces.Get(space.Id);
            Assert.NotNull(space);

            await connection.Spaces.Remove(space.Id);

            space = await connection.Spaces.Get(space.Id);
            Assert.Null(space);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Delete_Non_Existing()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();

            // Act.
            var act = new Func<Task>(async () => await connection.Spaces.Remove(id));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act); 
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Change_Non_Existing()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // Act.
            var act = new Func<Task>(async () => await connection.Spaces.Change(id, name));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act); 
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_Spaces_Add_Already_Existing()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection();
            var account = await _testContext.TransportTestContext.AddUserAccount(connection);
            var name = Guid.NewGuid().ToString();
            var space = await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data);
            Assert.NotNull(space);

            // Act.
            var act = new Func<Task>(async () => await connection.Spaces.Add(account.Id, name, SpaceTemplate.Data));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act); 
        }
    }
}