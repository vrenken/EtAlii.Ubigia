namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using Xunit;

    [Trait("Technology", "Grpc")]
    public sealed class RootInitializerTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public RootInitializerTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RootInitializer_Initialize()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(context.Host.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(context.HostName, context.HostAddress);
            var logical = new LogicalContextFactory().Create(logicalContextConfiguration);

            var rootInitializer = new RootInitializer(fabric, logical.Entries);

            // Act.
            //context.Host.Infrastructure.RootInitializer.Initialize(space.Id, root)
            rootInitializer.Initialize(space.Id, root);

            // Assert.
        }

        [Fact]
        public async Task RootInitializer_Initialize_Check_Resulting_Root()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(context.Host.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(context.HostName, context.HostAddress);
            var logical = new LogicalContextFactory().Create(logicalContextConfiguration);

            var rootInitializer = new RootInitializer(fabric, logical.Entries);

            // Act.
            //context.Host.Infrastructure.RootInitializer.Initialize(space.Id, root)
            rootInitializer.Initialize(space.Id, root);

            // Assert.
            var registeredRoot = context.Host.Infrastructure.Roots.Get(space.Id, root.Id);
            Assert.NotEqual(Identifier.Empty, registeredRoot.Identifier);
            Assert.NotEqual(Guid.Empty, registeredRoot.Id);
        }

        [Fact]
        public async Task RootInitializer_Initialize_Check_Resulting_Entry()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();

            Assert.Equal(root.Identifier, Identifier.Empty);
            root = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotEqual(root.Identifier, Identifier.Empty);
            Assert.NotEqual(root.Id, Guid.Empty);

            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(context.Host.Storage);
            var fabric = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(context.HostName, context.HostAddress);
            var logical = new LogicalContextFactory().Create(logicalContextConfiguration);

            var rootInitializer = new RootInitializer(fabric, logical.Entries);

            // Act.
            //context.Host.Infrastructure.RootInitializer.Initialize(space.Id, root)
            rootInitializer.Initialize(space.Id, root);

            // Assert.
            var registeredRoot = context.Host.Infrastructure.Roots.Get(space.Id, root.Id);
            Assert.NotEqual(registeredRoot.Identifier, Identifier.Empty);
            Assert.NotEqual(registeredRoot.Id, Guid.Empty);
            Assert.Equal(registeredRoot.Identifier, root.Identifier);

            var entry = context.Host.Infrastructure.Entries.Get(registeredRoot.Identifier);
            Assert.NotNull(entry);
        }
    }
}
