// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineExecutionPipelinesTests
    {
        [Fact]
        public void Pipeline_Execute_Pipelines_Integers_1()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerPipeline1())
                .ContinueWith(new IntegerPipeline2())
                .EndWith(i => i.ToString());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal("50", result);
        }

        [Fact]
        public void Pipeline_Execute_Pipelines_Integers_2()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            var pipeline = new Pipeline<int, int>()
                .StartWith(new IntegerPipeline1())
                .EndWith(new IntegerPipeline2());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal(50, result);
        }
    }
}
