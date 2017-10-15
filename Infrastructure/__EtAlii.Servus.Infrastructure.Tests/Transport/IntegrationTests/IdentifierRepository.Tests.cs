namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IdentifierRepository_Tests : TestBase
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
        public void IdentifierRepository_Get_Current_Head()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);

            var identifier = Infrastructure.Identifiers.GetCurrentHead(space.Id);
            Assert.AreNotEqual(identifier, Identifier.Empty);
        }

        [TestMethod]
        public void IdentifierRepository_Get_Next_Head()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);

            Identifier previousHeadIdentifier;
            var identifier = Infrastructure.Identifiers.GetNextHead(space.Id, out previousHeadIdentifier);
            Assert.AreNotEqual(identifier, Identifier.Empty);
            Assert.AreNotEqual(previousHeadIdentifier, Identifier.Empty);
            Assert.AreNotEqual(identifier, previousHeadIdentifier);
        }

        [TestMethod]
        public void IdentifierRepository_Get_Current_Tail()
        {
            var space = ApiTestHelper.CreateSpace(Infrastructure);

            var identifier = Infrastructure.Identifiers.GetTail(space.Id);
            Assert.AreNotEqual(identifier, Identifier.Empty);
        }
    }
}
