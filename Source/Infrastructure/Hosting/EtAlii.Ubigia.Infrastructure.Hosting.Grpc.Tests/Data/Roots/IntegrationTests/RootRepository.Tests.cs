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
	[Trait("Technology", "Grpc")]
    public class RootRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public RootRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RootRepository_Add()
        {
	        // Arrange.
	        var context = _testContext.Host;
			var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();
            var addedRoot = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);
        }

        [Fact]
        public async Task RootRepository_Get_By_Id()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();
            var addedRoot = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = await context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
            Assert.Equal(addedRoot.Id, fetchedRoot.Id);
            Assert.Equal(addedRoot.Name, fetchedRoot.Name);

            Assert.Equal(root.Id, fetchedRoot.Id);
            Assert.Equal(root.Name, fetchedRoot.Name);
        }

        [Fact]
        public async Task RootRepository_Get_By_Name()
        {
	        // Arrange.
	        var context = _testContext.Host;
			var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();
            var addedRoot = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = await context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Name).ConfigureAwait(false);
            Assert.Equal(addedRoot.Id, fetchedRoot.Id);
            Assert.Equal(addedRoot.Name, fetchedRoot.Name);

            Assert.Equal(root.Id, fetchedRoot.Id);
            Assert.Equal(root.Name, fetchedRoot.Name);
        }

        [Fact]
        public async Task RootRepository_Remove_By_Id()
        {
	        // Arrange.
	        var context = _testContext.Host;
			var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();
            var addedRoot = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = await context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedRoot);

            context.Host.Infrastructure.Roots.Remove(space.Id, addedRoot.Id);

            fetchedRoot = await context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
            Assert.Null(fetchedRoot);
        }

        [Fact]
        public async Task RootRepository_Remove_By_Instance()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();
            var addedRoot = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = await context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
            Assert.NotNull(fetchedRoot);

            context.Host.Infrastructure.Roots.Remove(space.Id, addedRoot);

            fetchedRoot = await context.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id).ConfigureAwait(false);
            Assert.Null(fetchedRoot);
        }

        [Fact]
        public async Task RootRepository_Get_Null()
        {
	        // Arrange.
	        var context = _testContext.Host;
			var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = await context.Host.Infrastructure.Roots.Get(space.Id, Guid.NewGuid()).ConfigureAwait(false);
            Assert.Null(root);
        }

        [Fact]
        public async Task RootRepository_GetAll()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);
            var root = _infrastructureTestHelper.CreateRoot();
            var addedRoot1 = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);
            root = _infrastructureTestHelper.CreateRoot();
            var addedRoot2 = await context.Host.Infrastructure.Roots.Add(space.Id, root).ConfigureAwait(false);

            // Act.
            var roots = await context.Host.Infrastructure.Roots
	            .GetAll(space.Id)
	            .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(addedRoot1);
            Assert.NotNull(addedRoot2);
            Assert.NotNull(roots);
            Assert.True(roots.Length >= 2);
        }
    }
}
