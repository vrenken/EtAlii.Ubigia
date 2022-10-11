// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
	using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
	public sealed class SpaceRepositoryTests : IClassFixture<FunctionalInfrastructureUnitTestContext>
    {
        private readonly FunctionalInfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public SpaceRepositoryTests(FunctionalInfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SpaceRepository_Add()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional, false).ConfigureAwait(false);
            var addedSpace = await _testContext.Functional.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);
        }

        [Fact]
        public async Task SpaceRepository_Get()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional, false).ConfigureAwait(false);
            var addedSpace = await _testContext.Functional.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = await _testContext.Functional.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.Equal(addedSpace.Id, fetchedSpace.Id);
            Assert.Equal(addedSpace.Name, fetchedSpace.Name);

            Assert.Equal(space.Id, fetchedSpace.Id);
            Assert.Equal(space.Name, fetchedSpace.Name);
        }

        [Fact]
        public async Task SpaceRepository_Remove_By_Id()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional, false).ConfigureAwait(false);
            var addedSpace = await _testContext.Functional.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = await _testContext.Functional.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedSpace);

            // Act.
            await _testContext.Functional.Spaces.Remove(addedSpace.Id).ConfigureAwait(false);

            // Assert.
            fetchedSpace = await _testContext.Functional.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public async Task SpaceRepository_Remove_By_Instance()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional, false).ConfigureAwait(false);
            var addedSpace = await _testContext.Functional.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            Assert.NotNull(addedSpace);
            Assert.NotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = await _testContext.Functional.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedSpace);

            // Act.
            await _testContext.Functional.Spaces.Remove(addedSpace).ConfigureAwait(false);

            // Assert.
            fetchedSpace = await _testContext.Functional.Spaces.Get(addedSpace.Id).ConfigureAwait(false);
            Assert.Null(fetchedSpace);
        }

        [Fact]
        public async Task SpaceRepository_Get_Null()
        {
			// Arrange.

            // Act.
            var space = await _testContext.Functional.Spaces.Get(Guid.NewGuid()).ConfigureAwait(false);

            // Assert.
            Assert.Null(space);
        }

        [Fact]
        public async Task SpaceRepository_GetAll()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional, false).ConfigureAwait(false);
            var addedSpace1 = await _testContext.Functional.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional, false).ConfigureAwait(false);
            var addedSpace2 = await _testContext.Functional.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);

            // Act.
            var spaces = await _testContext.Functional.Spaces
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
