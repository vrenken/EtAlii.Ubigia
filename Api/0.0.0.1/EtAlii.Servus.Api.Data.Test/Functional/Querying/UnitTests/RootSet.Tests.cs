namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class RootSet_Tests
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
        public void RootSet_New()
        {
            // Arrange.

            // Act.
            new RootSet(null);

            // Assert.
        }
    }
}