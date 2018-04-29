namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests
{
    using Xunit;
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;


	public sealed class SpaceRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public SpaceRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void SpaceRepository_Add()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);
        }

        [Fact]
        public void SpaceRepository_Get()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.Equal(addedSpace.Id, fetchedSpace.Id);
            Assert.Equal(addedSpace.Name, fetchedSpace.Name);

            Assert.Equal(space.Id, fetchedSpace.Id);
            Assert.Equal(space.Name, fetchedSpace.Name);
        }

        [Fact]
        public void SpaceRepository_Remove_By_Id()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.NotNull(fetchedSpace);

            context.Host.Infrastructure.Spaces.Remove(addedSpace.Id);

            fetchedSpace = context.Host.Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public void SpaceRepository_Remove_By_Instance()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
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
        public void SpaceRepository_GetAll()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            var addedSpace = context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false);
            addedSpace = context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);

            var spaces = context.Host.Infrastructure.Spaces.GetAll();
            Assert.NotNull(spaces);
            Assert.True(spaces.Count() >= 4);
        }
    }
}
