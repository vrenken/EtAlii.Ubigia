namespace EtAlii.Ubigia.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using Xunit;
    using System;
    using System.Linq;

    
    public class RootRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public RootRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void RootRepository_Add()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);
        }

        [Fact]
        public void RootRepository_Get_By_Id()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.Equal(addedRoot.Id, fetchedRoot.Id);
            Assert.Equal(addedRoot.Name, fetchedRoot.Name);

            Assert.Equal(root.Id, fetchedRoot.Id);
            Assert.Equal(root.Name, fetchedRoot.Name);
        }

        [Fact]
        public void RootRepository_Get_By_Name()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Name);
            Assert.Equal(addedRoot.Id, fetchedRoot.Id);
            Assert.Equal(addedRoot.Name, fetchedRoot.Name);

            Assert.Equal(root.Id, fetchedRoot.Id);
            Assert.Equal(root.Name, fetchedRoot.Name);
        }

        [Fact]
        public void RootRepository_Remove_By_Id()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.NotNull(fetchedRoot);

            _testContext.HostTestContext.Host.Infrastructure.Roots.Remove(space.Id, addedRoot.Id);

            fetchedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.Null(fetchedRoot);
        }

        [Fact]
        public void RootRepository_Remove_By_Instance()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            Assert.NotNull(addedRoot);
            Assert.NotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.NotNull(fetchedRoot);

            _testContext.HostTestContext.Host.Infrastructure.Roots.Remove(space.Id, addedRoot);

            fetchedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.Null(fetchedRoot);
        }

        [Fact]
        public void RootRepository_Get_Null()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = _testContext.HostTestContext.Host.Infrastructure.Roots.Get(space.Id, Guid.NewGuid());
            Assert.Null(root);
        }

        [Fact]
        public void RootRepository_GetAll()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);
            var root = InfrastructureTestHelper.CreateRoot();
            var addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);
            root = InfrastructureTestHelper.CreateRoot();
            addedRoot = _testContext.HostTestContext.Host.Infrastructure.Roots.Add(space.Id, root);

            var roots = _testContext.HostTestContext.Host.Infrastructure.Roots.GetAll(space.Id);
            Assert.NotNull(roots);
            Assert.True(roots.Count() >= 2);
        }
    }
}
