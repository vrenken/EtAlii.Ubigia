﻿namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class GraphPathAssigner_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathAssigner_Create()
        {
            // Arrange.
            var context = new AssignmentContext(null);

            // Act.
            var assigner = new GraphPathAssigner(context, null);

            // Assert.
        }
    }
}
