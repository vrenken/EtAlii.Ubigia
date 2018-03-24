//namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;
//    using EtAlii.Ubigia.Api.Functional;
//    using EtAlii.Ubigia.Api.Logical;
//    using EtAlii.Ubigia.Infrastructure.Hosting;
//    using Xunit;
//    using TestAssembly = EtAlii.Ubigia.Infrastructure.Hosting.TestAssembly;

    
//    public class SystemConnection_Tests : IClassFixture<HostUnitTestContext>
//    {
//        private readonly HostUnitTestContext _testContext;

//        public SystemConnection_Tests(HostUnitTestContext testContext)
//        {
//            _testContext = testContext;
//        }

//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SystemConnection_Create()
//        {
//            // Arrange.

//            // Act.
//            var connection = await _testContext.HostTestContext.CreateSystemConnection();

//            // Assert.
//            Assert.NotNull(connection);
//        }

//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SystemConnection_Create_DataConnection()
//        {
//            // Arrange.
//            var userName = "TestUser";
//            var password = "123";
//            var spaceName = "TestSpace";
//            var systemConnection = await _testContext.HostTestContext.CreateSystemConnection();
//            await _testContext.HostTestContext.AddUserAccountAndSpaces(systemConnection, userName, password, new[] { spaceName });

//            // Act.
//            var connection = await systemConnection.OpenSpace("TestUser", "TestSpace");

//            // Assert.
//            Assert.NotNull(connection);
//            Assert.NotNull(connection.Storage);
//            Assert.NotNull(connection.Account);
//            Assert.Equal(userName, connection.Account.Name);
//            Assert.NotNull(connection.Space);
//            Assert.Equal(spaceName, connection.Space.Name);
//        }

//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SystemConnection_Create_ManagementConnection()
//        {
//            // Arrange.
//            var userName = Guid.NewGuid().ToString();// "TestUser";
//            var password = "123";
//            var spaceName = "TestSpace";
//            var systemConnection = await _testContext.HostTestContext.CreateSystemConnection();
//            await _testContext.HostTestContext.AddUserAccountAndSpaces(systemConnection, userName, password, new[] { spaceName });

//            // Act.
//            var connection = await systemConnection.OpenManagementConnection();

//            // Assert.
//            Assert.NotNull(connection);
//            Assert.NotNull(connection.Storage);
//            var account = await connection.Accounts.Get(userName);
//            Assert.NotNull(account);
//            Assert.Equal(userName, account.Name);
//            var space = await connection.Spaces.Get(account.Id, spaceName);
//            Assert.NotNull(space);
//            Assert.Equal(spaceName, space.Name);
//        }

//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SystemConnection_Advanced_Operation_Single_Space_01()
//        {
//            // Arrange.
//            var accountName = Guid.NewGuid().ToString();
//            var password = Guid.NewGuid().ToString();
//            var spaceName = Guid.NewGuid().ToString();

//            var systemConnection = await _testContext.HostTestContext.CreateSystemConnection();
//            await _testContext.HostTestContext.AddUserAccountAndSpaces(systemConnection, accountName, password, new[] { spaceName });

//            var dataConnection = await systemConnection.OpenSpace(accountName, spaceName);

//            var dataContextconfiguration = new DataContextConfiguration()
//                .Use(dataConnection);

//            var dataContext = new DataContextFactory().Create(dataContextconfiguration);

//            var addQueries = new[]
//            {
//                "/Person+=Doe/John",
//                "/Person+=Doe/Jane",
//                "/Person+=Doe/Johnny",
//            };

//            var addQuery = String.Join("\r\n", addQueries);
//            var selectQuery = "<= Count() <= /Person/Doe/*";

//            var addScript = dataContext.Scripts.Parse(addQuery).Script;
//            var selectScript = dataContext.Scripts.Parse(selectQuery).Script;
//            var scope = new ScriptScope();

//            // Act.
//            var lastSequence = await dataContext.Scripts.Process(addScript, scope);
//            await lastSequence.Output.ToArray();
//            lastSequence = await dataContext.Scripts.Process(selectScript, scope);
//            var personsAfter = await lastSequence.Output.ToArray();

//            // Assert.
//            Assert.NotNull(personsAfter);
//            Assert.Single(personsAfter);
//            Assert.Equal(3, personsAfter.Single());
//        }

//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SystemConnection_Advanced_Operation_Single_Space_02()
//        {
//            // Arrange.
//            var accountName = Guid.NewGuid().ToString();
//            var password = Guid.NewGuid().ToString();
//            var spaceName = Guid.NewGuid().ToString();

//            var systemConnection = await _testContext.HostTestContext.CreateSystemConnection();
//            await _testContext.HostTestContext.AddUserAccountAndSpaces(systemConnection, accountName, password, new[] { spaceName });

//            var dataConnection = await systemConnection.OpenSpace(accountName, spaceName);

//            var dataContextconfiguration = new DataContextConfiguration()
//                .Use(dataConnection);

//            var dataContext = new DataContextFactory().Create(dataContextconfiguration);

//            var selectQuery = "<= /Person";

//            var selectScript = dataContext.Scripts.Parse(selectQuery).Script;
//            var scope = new ScriptScope();

//            // Act.
//            var lastSequence = await dataContext.Scripts.Process(selectScript, scope);
//            var item = await lastSequence.Output.ToArray();

//            // Assert.
//            Assert.Single(item);
//            Assert.IsAssignableFrom<INode>(item[0]);
//        }
//    }
//}
