namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Content = EtAlii.Servus.Api.Content;

    [TestClass]
    public class Content_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Content_Create()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Content_Stored_Defaults_To_False()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
            Assert.IsFalse(content.Stored);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Content_Summary_Defaults_To_Null()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
            Assert.IsNull(content.Summary);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Content_TotalParts_Defaults_To_0()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
            Assert.AreEqual((UInt64)0, content.TotalParts);
        }
    }
}
