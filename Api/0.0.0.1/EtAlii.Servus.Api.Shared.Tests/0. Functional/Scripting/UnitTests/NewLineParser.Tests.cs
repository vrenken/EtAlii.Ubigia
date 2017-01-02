namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moppet.Lapa;

    [TestClass]
    public class NewLineParser_Tests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Create()
        {
            // Arrange.

            // Act.
            var parser = new NewLineParser();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Single_Newline()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n");

            // Assert.
            Assert.IsTrue(result.Success);
            Assert.AreEqual(String.Empty, result.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Leading_Space()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do(" \n");

            // Assert.
            Assert.IsTrue(result.Success);
            Assert.AreEqual(String.Empty, result.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Following_Space()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n ");

            // Assert.
            Assert.IsTrue(result.Success);
            Assert.AreEqual(String.Empty, result.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Leading_And_Following_Space()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do(" \n ");

            // Assert.
            Assert.IsTrue(result.Success);
            Assert.AreEqual(String.Empty, result.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n\n");

            // Assert.
            Assert.IsTrue(result.Success);
            Assert.AreEqual(String.Empty, result.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple_With_Space_Inbetween()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = parser.OptionalMultiple.Do("\n \n");

            // Assert.
            Assert.IsTrue(result.Success);
            Assert.AreEqual(String.Empty, result.Rest.ToString());
        }
    }
}