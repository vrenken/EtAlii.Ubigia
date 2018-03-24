//namespace EtAlii.Ubigia.Infrastructure.Hosting.IntegrationTests
//{
//    using Xunit;
//    using System;
//    using System.Linq;
//    using EtAlii.Ubigia.Api.Transport;
//    using EtAlii.Ubigia.Infrastructure;

    
//    public sealed class SpaceRepository_Tests : IClassFixture<HostUnitTestContext>
//    {
//        private readonly HostUnitTestContext _testContext;

//        public SpaceRepository_Tests(HostUnitTestContext testContext)
//        {
//            _testContext = testContext;
//        }

//        [Fact]
//        public void SpaceRepository_Add()
//        {
//            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure, false);
//            var addedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
//            Assert.NotNull(addedSpace);
//            Assert.NotEqual(addedSpace.Id, Guid.Empty);
//        }

//        [Fact]
//        public void SpaceRepository_Get()
//        {
//            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure, false);
//            var addedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
//            Assert.NotNull(addedSpace);
//            Assert.NotEqual(addedSpace.Id, Guid.Empty);

//            var fetchedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Get(addedSpace.Id);
//            Assert.Equal(addedSpace.Id, fetchedSpace.Id);
//            Assert.Equal(addedSpace.Name, fetchedSpace.Name);

//            Assert.Equal(space.Id, fetchedSpace.Id);
//            Assert.Equal(space.Name, fetchedSpace.Name);
//        }

//        [Fact]
//        public void SpaceRepository_Remove_By_Id()
//        {
//            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure, false);
//            var addedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
//            Assert.NotNull(addedSpace);
//            Assert.NotEqual(addedSpace.Id, Guid.Empty);

//            var fetchedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Get(addedSpace.Id);
//            Assert.NotNull(fetchedSpace);

//            _testContext.HostTestContext.Host.Infrastructure.Spaces.Remove(addedSpace.Id);

//            fetchedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Get(addedSpace.Id);
//            Assert.Null(fetchedSpace);
//        }

//        [Fact]
//        public void SpaceRepository_Remove_By_Instance()
//        {
//            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure, false);
//            var addedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
//            Assert.NotNull(addedSpace);
//            Assert.NotEqual(addedSpace.Id, Guid.Empty);

//            var fetchedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Get(addedSpace.Id);
//            Assert.NotNull(fetchedSpace);

//            _testContext.HostTestContext.Host.Infrastructure.Spaces.Remove(addedSpace);

//            fetchedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Get(addedSpace.Id);
//            Assert.Null(fetchedSpace);
//        }

//        [Fact]
//        public void SpaceRepository_Get_Null()
//        {
//            var space = _testContext.HostTestContext.Host.Infrastructure.Spaces.Get(Guid.NewGuid());
//            Assert.Null(space);
//        }

//        [Fact]
//        public void SpaceRepository_GetAll()
//        {
//            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure, false);
//            var addedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);
//            space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure, false);
//            addedSpace = _testContext.HostTestContext.Host.Infrastructure.Spaces.Add(space, SpaceTemplate.Data);

//            var spaces = _testContext.HostTestContext.Host.Infrastructure.Spaces.GetAll();
//            Assert.NotNull(spaces);
//            Assert.True(spaces.Count() >= 4);
//        }
//    }
//}
