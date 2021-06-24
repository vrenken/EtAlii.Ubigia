// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineExecutionActionBasedTests
    {
        [Fact]
        public void Pipeline_Execute_ActionBased_Integers_1()
        {
            // Arrange.
            var first = 0;
            var second = 0;
            var third = 0;

            var pipeline = new Pipeline<int>()
                .StartWith(i => first = i + 1)
                .ContinueWith(i => second = i + 2)
                .EndWith(i => third = i + 3);

            // Act.
            pipeline.Process(3);

            // Assert.
            Assert.Equal(4, first);
            Assert.Equal(6, second);
            Assert.Equal(9, third);
        }
    }
}
