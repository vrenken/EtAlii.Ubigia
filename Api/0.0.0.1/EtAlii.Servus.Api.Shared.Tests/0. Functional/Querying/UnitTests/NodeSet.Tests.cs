﻿namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NodeSet_Tests
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
        public void NodeSet_New()
        {
            // Arrange.

            // Act.
            new NodeSet(null, null, null);

            // Assert.
        }
    }
}