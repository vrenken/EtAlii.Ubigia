// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
	using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
	public sealed class SpaceRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public SpaceRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SpaceRepository_Add()
        {
			// Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false).ConfigureAwait(false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);
        }

        [Fact]
        public async Task SpaceRepository_Get()
        {
			// Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false).ConfigureAwait(false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = await context.Host.Infrastructure.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.Equal(addedSpace.Id, fetchedSpace.Id);
            Assert.Equal(addedSpace.Name, fetchedSpace.Name);

            Assert.Equal(space.Id, fetchedSpace.Id);
            Assert.Equal(space.Name, fetchedSpace.Name);
        }

        [Fact]
        public async Task SpaceRepository_Remove_By_Id()
        {
			// Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false).ConfigureAwait(false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = await context.Host.Infrastructure.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedSpace);

            // Act.
            await context.Host.Infrastructure.Spaces.Remove(addedSpace.Id).ConfigureAwait(false);

            // Assert.
            fetchedSpace = await context.Host.Infrastructure.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public async Task SpaceRepository_Remove_By_Instance()
        {
			// Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false).ConfigureAwait(false);
            var addedSpace = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = await context.Host.Infrastructure.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedSpace);

            // Act.
            await context.Host.Infrastructure.Spaces.Remove(addedSpace).ConfigureAwait(false);

            // Assert.
            fetchedSpace = await context.Host.Infrastructure.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public async Task SpaceRepository_Get_Null()
        {
			// Arrange.
	        var context = _testContext.Host;

            // Act.
            var space = await context.Host.Infrastructure.Spaces.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceRepository_GetAll()
        {
			// Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false).ConfigureAwait(false);
            var addedSpace1 = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure, false).ConfigureAwait(false);
            var addedSpace2 = await context.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);

            // Act.
            var spaces = await context.Host.Infrastructure.Spaces
	            .GetAll()
	            .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedSpace1);
            Assert.NotNull(addedSpace2);
            Assert.NotNull(spaces);
            Assert.True(spaces.Length >= 4);
        }
    }
}
