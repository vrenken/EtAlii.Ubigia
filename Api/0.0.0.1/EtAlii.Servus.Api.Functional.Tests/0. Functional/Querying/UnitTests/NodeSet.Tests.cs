﻿namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Xunit;


    public class NodeSet_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeSet_New()
        {
            // Arrange.

            // Act.
            new NodeSet(null, null, null);

            // Assert.
        }
    }
}