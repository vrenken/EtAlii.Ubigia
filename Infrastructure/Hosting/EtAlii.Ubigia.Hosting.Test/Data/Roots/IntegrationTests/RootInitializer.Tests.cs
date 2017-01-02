namespace EtAlii.Ubigia.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using Xunit;
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;

    
    public sealed class RootInitializer_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public RootInitializer_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void RootInitializer_Initialize()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(_testContext.HostTestContext.Host.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(_testContext.HostTestContext.Host.Infrastructure.Configuration.Name, _testContext.HostTestContext.Host.Infrastructure.Configuration.Address);
            var logical = new LogicalContextFactory().Create(logicalContextConfiguration);

            var rootInitializer = new RootInitializer(logical);

            // Act.
            //_testContext.HostTestContext.Host.Infrastructure.RootInitializer.Initialize(space.Id, root);
            rootInitializer.Initialize(space.Id, root);

            // Assert.
        }

        [Fact]
        public void RootInitializer_Initialize_Check_Resulting_Root()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(_testContext.HostTestContext.Host.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(_testContext.HostTestContext.Host.Infrastructure.Configuration.Name, _testContext.HostTestContext.Host.Infrastructure.Configuration.Address);
            var logical = new LogicalContextFactory().Create(logicalContextConfiguration);

            var rootInitializer = new RootInitializer(logical);

            // Act.
            //_testContext.HostTestContext.Host.Infrastructure.RootInitializer.Initialize(space.Id, root);
            rootInitializer.Initialize(space.Id, root);

            // Assert.
            var registeredRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, root.Id);
            Assert.NotEqual(Identifier.Empty, registeredRoot.Identifier);
            Assert.NotEqual(Guid.Empty, registeredRoot.Id);
        }

        [Fact]
        public void RootInitializer_Initialize_Check_Resulting_Entry()
        {
            // Arrange.
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(_testContext.HostTestContext.Host.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(_testContext.HostTestContext.Host.Infrastructure.Configuration.Name, _testContext.HostTestContext.Host.Infrastructure.Configuration.Address);
            var logical = new LogicalContextFactory().Create(logicalContextConfiguration);

            var rootInitializer = new RootInitializer(logical);

            // Act.
            //_testContext.HostTestContext.Host.Infrastructure.RootInitializer.Initialize(space.Id, root);
            rootInitializer.Initialize(space.Id, root);

            // Assert.
            var registeredRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, root.Id);
            Assert.NotEqual(registeredRoot.Identifier, Identifier.Empty);
            Assert.NotEqual(registeredRoot.Id, Guid.Empty);
            Assert.Equal(registeredRoot.Identifier, root.Identifier);

            var entry = _testContext.HostTestContext.Host.Infrastructure.Entries.Get(registeredRoot.Identifier);
            Assert.NotNull(entry);
        }
    }
}
