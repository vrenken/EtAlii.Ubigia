namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
	using Xunit;

	[Trait("Technology", "NetCore")]
	public sealed class SpaceRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public SpaceRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SpaceRepository_Add()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);
        }

        [Fact]
        public async Task SpaceRepository_Get()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.Equal(addedSpace.Id, fetchedSpace.Id);
            Assert.Equal(addedSpace.Name, fetchedSpace.Name);

            Assert.Equal(space.Id, fetchedSpace.Id);
            Assert.Equal(space.Name, fetchedSpace.Name);
        }

        [Fact]
        public async Task SpaceRepository_Remove_By_Id()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.NotNull(fetchedSpace);

            context.Host.Infrastructure.Spaces.Remove(addedSpace.Id);

            fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public async Task SpaceRepository_Remove_By_Instance()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.NotNull(fetchedSpace);

            context.Host.Infrastructure.Spaces.Remove(addedSpace);

            fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public void SpaceRepository_Get_Null()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = context.Host.Infrastructure.Spaces.Get(Guid.NewGuid());
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceRepository_GetAll()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace1 = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace2 = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);

            // Act.
            var spaces = context.Host.Infrastructure.Spaces.GetAll();
            
            // Assert.
            Assert.NotNull(addedSpace1);
            Assert.NotNull(addedSpace2);
            Assert.NotNull(spaces);
            Assert.True(spaces.Count() >= 4);
        }
    }
}
