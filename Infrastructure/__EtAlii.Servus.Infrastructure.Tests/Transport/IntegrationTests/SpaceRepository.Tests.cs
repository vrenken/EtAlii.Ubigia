namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public sealed class SpaceRepository_Tests : TestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void SpaceRepository_Add()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure, false);
            var addedSpace = Infrastructure.Spaces.Add(space);
            Assert.IsNotNull(addedSpace);
            Assert.AreNotEqual(addedSpace.Id, Guid.Empty);
        }

        [TestMethod]
        public void SpaceRepository_Get()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure, false);
            var addedSpace = Infrastructure.Spaces.Add(space);
            Assert.IsNotNull(addedSpace);
            Assert.AreNotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.AreEqual(addedSpace.Id, fetchedSpace.Id);
            Assert.AreEqual(addedSpace.Name, fetchedSpace.Name);

            Assert.AreEqual(space.Id, fetchedSpace.Id);
            Assert.AreEqual(space.Name, fetchedSpace.Name);
        }

        [TestMethod]
        public void SpaceRepository_Remove_By_Id()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure, false);
            var addedSpace = Infrastructure.Spaces.Add(space);
            Assert.IsNotNull(addedSpace);
            Assert.AreNotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.IsNotNull(fetchedSpace);

            Infrastructure.Spaces.Remove(addedSpace.Id);

            fetchedSpace = Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.IsNull(fetchedSpace);
        }

        [TestMethod]
        public void SpaceRepository_Remove_By_Instance()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure, false);
            var addedSpace = Infrastructure.Spaces.Add(space);
            Assert.IsNotNull(addedSpace);
            Assert.AreNotEqual(addedSpace.Id, Guid.Empty);

            var fetchedSpace = Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.IsNotNull(fetchedSpace);

            Infrastructure.Spaces.Remove(addedSpace);

            fetchedSpace = Infrastructure.Spaces.Get(addedSpace.Id);
            Assert.IsNull(fetchedSpace);
        }

        [TestMethod]
        public void SpaceRepository_Get_Null()
        {
            var space = Infrastructure.Spaces.Get(Guid.NewGuid());
            Assert.IsNull(space);
        }

        [TestMethod]
        public void SpaceRepository_GetAll()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure, false);
            var addedSpace = Infrastructure.Spaces.Add(space);
            space = ApiTestHelper.CreateSpace(Infrastructure, false);
            addedSpace = Infrastructure.Spaces.Add(space);

            var spaces = Infrastructure.Spaces.GetAll();
            Assert.IsNotNull(spaces);
            Assert.IsTrue(spaces.Count() >= 2);
        }
    }
}
