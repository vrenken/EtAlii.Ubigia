// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using Xunit;

    public class DowndateOfPathSubjectPartTests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PreviousSubjectPart_ToString()
        {
            // Arrange.
            var part = new DowndatePathSubjectPart();

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("{", result);
        }
    }
}
