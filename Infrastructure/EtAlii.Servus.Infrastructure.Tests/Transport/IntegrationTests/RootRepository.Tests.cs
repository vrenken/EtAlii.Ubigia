namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class RootRepository_Tests : TestBase
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
        public void RootRepository_Add()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();
            var addedRoot = Infrastructure.Roots.Add(space.Id, root);
            Assert.IsNotNull(addedRoot);
            Assert.AreNotEqual(addedRoot.Id, Guid.Empty);
        }

        [TestMethod]
        public void RootRepository_Get_By_Id()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();
            var addedRoot = Infrastructure.Roots.Add(space.Id, root);
            Assert.IsNotNull(addedRoot);
            Assert.AreNotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.AreEqual(addedRoot.Id, fetchedRoot.Id);
            Assert.AreEqual(addedRoot.Name, fetchedRoot.Name);

            Assert.AreEqual(root.Id, fetchedRoot.Id);
            Assert.AreEqual(root.Name, fetchedRoot.Name);
        }

        [TestMethod]
        public void RootRepository_Get_By_Name()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();
            var addedRoot = Infrastructure.Roots.Add(space.Id, root);
            Assert.IsNotNull(addedRoot);
            Assert.AreNotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = Infrastructure.Roots.Get(space.Id, addedRoot.Name);
            Assert.AreEqual(addedRoot.Id, fetchedRoot.Id);
            Assert.AreEqual(addedRoot.Name, fetchedRoot.Name);

            Assert.AreEqual(root.Id, fetchedRoot.Id);
            Assert.AreEqual(root.Name, fetchedRoot.Name);
        }

        [TestMethod]
        public void RootRepository_Remove_By_Id()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();
            var addedRoot = Infrastructure.Roots.Add(space.Id, root);
            Assert.IsNotNull(addedRoot);
            Assert.AreNotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.IsNotNull(fetchedRoot);

            Infrastructure.Roots.Remove(space.Id, addedRoot.Id);

            fetchedRoot = Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.IsNull(fetchedRoot);
        }

        [TestMethod]
        public void RootRepository_Remove_By_Instance()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();
            var addedRoot = Infrastructure.Roots.Add(space.Id, root);
            Assert.IsNotNull(addedRoot);
            Assert.AreNotEqual(addedRoot.Id, Guid.Empty);

            var fetchedRoot = Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.IsNotNull(fetchedRoot);

            Infrastructure.Roots.Remove(space.Id, addedRoot);

            fetchedRoot = Infrastructure.Roots.Get(space.Id, addedRoot.Id);
            Assert.IsNull(fetchedRoot);
        }

        [TestMethod]
        public void RootRepository_Get_Null()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = Infrastructure.Roots.Get(space.Id, Guid.NewGuid());
            Assert.IsNull(root);
        }

        [TestMethod]
        public void RootRepository_GetAll()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();
            var addedRoot = Infrastructure.Roots.Add(space.Id, root);
            root = ApiTestHelper.CreateRoot();
            addedRoot = Infrastructure.Roots.Add(space.Id, root);

            var roots = Infrastructure.Roots.GetAll(space.Id);
            Assert.IsNotNull(roots);
            Assert.IsTrue(roots.Count() >= 2);
        }
    }
}
