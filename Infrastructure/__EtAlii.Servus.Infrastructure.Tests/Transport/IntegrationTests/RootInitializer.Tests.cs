namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public sealed class RootInitializer_Tests : TestBase
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
        public void RootInitializer_Initialize()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();

            Assert.AreEqual(root.Identifier, Identifier.Empty);
            root = Infrastructure.Roots.Add(space.Id, root);
            Assert.AreNotEqual(root.Identifier, Identifier.Empty);
            Assert.AreNotEqual(root.Id, Guid.Empty);

            Infrastructure.RootInitializer.Initialize(space.Id, root);
        }

        [TestMethod]
        public void RootInitializer_Initialize_Check_Resulting_Root()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();

            Assert.AreEqual(root.Identifier, Identifier.Empty);
            root = Infrastructure.Roots.Add(space.Id, root);
            Assert.AreNotEqual(root.Identifier, Identifier.Empty);
            Assert.AreNotEqual(root.Id, Guid.Empty);

            Infrastructure.RootInitializer.Initialize(space.Id, root);

            var registeredRoot = Infrastructure.Roots.Get(space.Id, root.Id);
            Assert.AreNotEqual(Identifier.Empty, registeredRoot.Identifier);
            Assert.AreNotEqual(Guid.Empty, registeredRoot.Id);
        }

        [TestMethod]
        public void RootInitializer_Initialize_Check_Resulting_Entry()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var root = ApiTestHelper.CreateRoot();

            Assert.AreEqual(root.Identifier, Identifier.Empty);
            root = Infrastructure.Roots.Add(space.Id, root);
            Assert.AreNotEqual(root.Identifier, Identifier.Empty);
            Assert.AreNotEqual(root.Id, Guid.Empty);

            Infrastructure.RootInitializer.Initialize(space.Id, root);

            var registeredRoot = Infrastructure.Roots.Get(space.Id, root.Id);
            Assert.AreNotEqual(registeredRoot.Identifier, Identifier.Empty);
            Assert.AreNotEqual(registeredRoot.Id, Guid.Empty);
            Assert.AreEqual(registeredRoot.Identifier, root.Identifier);

            var entry = Infrastructure.Entries.Get(registeredRoot.Identifier);
            Assert.IsNotNull(entry);
        }
    }
}
