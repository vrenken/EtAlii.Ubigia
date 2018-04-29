﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
	using Xunit;
    using System;
    using System.Linq;

    
    public class RootRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public RootRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void RootRepository_Add()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);
        }

        [Fact]
        public void RootRepository_Get_By_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
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
        public void RootRepository_Get_By_Name()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
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
        public void RootRepository_Remove_By_Id()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
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
        public void RootRepository_Remove_By_Instance()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
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
        public void RootRepository_Get_Null()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
			var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = context.Host.Infrastructure.Roots.Get(space.Id, Guid.NewGuid());
            Assert.Null(root);
        }

        [Fact]
        public void RootRepository_GetAll()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);
            root = InfrastructureTestHelper.CreateRoot();
            addedRoot = context.Host.Infrastructure.Roots.Add(space.Id, root);

            var roots = context.Host.Infrastructure.Roots.GetAll(space.Id);
            Assert.NotNull(roots);
            Assert.True(roots.Count() >= 2);
        }
    }
}
