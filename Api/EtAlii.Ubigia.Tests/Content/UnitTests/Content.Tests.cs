﻿namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class ContentTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Create()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Stored_Defaults_To_False()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
            Assert.False(content.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Summary_Defaults_To_Null()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
            Assert.Null(content.Summary);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_TotalParts_Defaults_To_0()
        {
            // Arrange.

            // Act.
            var content = new Content();

            // Assert.
            Assert.Equal((ulong)0, content.TotalParts);
        }
    }
}
