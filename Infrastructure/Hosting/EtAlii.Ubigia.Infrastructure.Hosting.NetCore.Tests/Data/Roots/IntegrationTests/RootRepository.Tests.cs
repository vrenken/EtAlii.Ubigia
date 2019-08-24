namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Xunit;

	[Trait("Technology", "NetCore")]
    public class RootRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public RootRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RootRepository_Add()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);
        }

        [Fact]
        public async Task RootRepository_Get_By_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.Equal(addedRoot.Id, fetchedRoot.Id);
            Assert.Equal(addedRoot.Name, fetchedRoot.Name);

            Assert.Equal(root.Id, fetchedRoot.Id);
            Assert.Equal(root.Name, fetchedRoot.Name);
        }

        [Fact]
        public async Task RootRepository_Get_By_Name()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Name);
            Assert.Equal(addedRoot.Id, fetchedRoot.Id);
            Assert.Equal(addedRoot.Name, fetchedRoot.Name);

            Assert.Equal(root.Id, fetchedRoot.Id);
            Assert.Equal(root.Name, fetchedRoot.Name);
        }

        [Fact]
        public async Task RootRepository_Remove_By_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.NotNull(fetchedRoot);

            context.Host.Infrastructure.Roots.Remove(space.Id, addedRoot.Id);

            fetchedRoot = context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.Null(fetchedRoot);
        }

        [Fact]
        public async Task RootRepository_Remove_By_Instance()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.NotNull(fetchedRoot);

            context.Host.Infrastructure.Roots.Remove(space.Id, addedRoot);

            fetchedRoot = context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.Null(fetchedRoot);
        }

        [Fact]
        public async Task RootRepository_Get_Null()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = context.Host.Infrastructure.Roots.Get(space.Id, Guid.NewGuid());
            Assert.Null(root);
        }

        [Fact]
        public async Task RootRepository_GetAll()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot1 = context.Host.Infrastructure.Roots.Add(space.Id, root);
            root = InfrastructureTestHelper.CreateRoot();
            var addedRoot2 = context.Host.Infrastructure.Roots.Add(space.Id, root);

            // Act.
            var roots = context.Host.Infrastructure.Roots.GetAll(space.Id);
            
            // Assert.
            Assert.NotNull(addedRoot1);
            Assert.NotNull(addedRoot2);
            Assert.NotNull(roots);
            Assert.True(roots.Count() >= 2);
        }
    }
}
