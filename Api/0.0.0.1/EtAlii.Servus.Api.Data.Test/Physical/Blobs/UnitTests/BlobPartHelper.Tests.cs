namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class BlobPartHelper_Tests
    {

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentDefinitionPart()
        {
            var blob = new ContentDefinitionPart();
            var name = BlobPartHelper.GetName(blob);
            Assert.AreEqual(@"ContentDefinition", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentDefinitionPart_Generic()
        {
            var name = BlobPartHelper.GetName<ContentDefinitionPart>();
            Assert.AreEqual(@"ContentDefinition", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentPart()
        {
            var blobPart = new ContentPart();
            var name = BlobPartHelper.GetName(blobPart);
            Assert.AreEqual(@"Content", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobPartHelper_SetId_ContentPart()
        {
            // Arrange.
            var blobPart = new ContentPart();
            var id = (uint)new Random().Next(0, int.MaxValue);

            // Act.
            BlobPartHelper.SetId(blobPart, id);

            // Assert.
            Assert.AreEqual(id, blobPart.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobPartHelper_GetName_ContentPart_Generic()
        {
            var name = BlobPartHelper.GetName<ContentPart>();
            Assert.AreEqual(@"Content", name);
        }
    }
}
