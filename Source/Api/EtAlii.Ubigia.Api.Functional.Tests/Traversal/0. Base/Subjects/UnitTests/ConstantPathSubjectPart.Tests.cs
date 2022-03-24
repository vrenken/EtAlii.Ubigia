// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using Xunit;

    public sealed class ConstantPathSubjectPartTests
    {

        [Fact]
        public void ConstantPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new ConstantPathSubjectPart("Test");

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("Test", result);
        }
    }
}
