// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineCreateTests
    {
        [Fact]
        public void Pipeline_Create_FunctionBased_Int_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, int>();

            // Assert.
            Assert.NotNull(pipeline);
        }

        [Fact]
        public void Pipeline_Create_FunctionBased_Int_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, string>();

            // Assert.
            Assert.NotNull(pipeline);
        }

        [Fact]
        public void Pipeline_Create_FunctionBased_String_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string, int>();

            // Assert.
            Assert.NotNull(pipeline);
        }


        [Fact]
        public void Pipeline_Create_ActionBased_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>();

            // Assert.
            Assert.NotNull(pipeline);
        }

        [Fact]
        public void Pipeline_Create_ActionBased_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>();

            // Assert.
            Assert.NotNull(pipeline);
        }
    }
}
