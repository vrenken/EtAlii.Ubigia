namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ContentPartDefinition_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPartDefinition_Create_Check_Checksum()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.AreEqual((UInt64)0L, contentDefinitionPart.Checksum);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPartDefinition_Create_Check_Id()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.AreEqual((UInt64)0L, contentDefinitionPart.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPartDefinition_Create_Check_Size()
        {
            var contentDefinitionPart = new ContentDefinitionPart();
            Assert.AreEqual((UInt64)0L, contentDefinitionPart.Size);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPartDefinition_Empty_Check_Size()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.AreEqual((UInt64)0L, contentDefinitionPart.Size);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPartDefinition_Empty_Check_Checksum()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.AreEqual((UInt64)0L, contentDefinitionPart.Checksum);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPartDefinition_Empty_Check_Id()
        {
            var contentDefinitionPart = ContentDefinitionPart.Empty;
            Assert.AreEqual((UInt64)0L, contentDefinitionPart.Id);
        }
    }
}
