// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using Xunit;

    public class VariablePathSubjectPartTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void VariablePathSubjectPart_ToString()
        {
            // Arrange.
            var part = new VariablePathSubjectPart("Test");

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("$Test", result);
        }
    }
}
