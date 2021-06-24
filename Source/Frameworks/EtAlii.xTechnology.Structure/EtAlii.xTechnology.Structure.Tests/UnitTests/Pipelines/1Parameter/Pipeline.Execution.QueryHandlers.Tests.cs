// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using System;
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineExecutionQueryHandlersTests
    {
        [Fact]
        public void Pipeline_Execute_QueryHandlers_StartWith()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int[]>>(registration);
        }

        [Fact]
        public void Pipeline_Execute_QueryHandlers_ContinueWith()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayToStringQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int[], string>>(registration);
        }

        [Fact]
        public void Pipeline_Execute_QueryHandlers_EndWith()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .EndWith(new IntegerArrayToStringQueryHandler());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            var resultArray = result.Split(new[] {", "}, StringSplitOptions.None);
            Assert.Equal("0", resultArray[0]);
            Assert.Equal("1", resultArray[1]);
            Assert.Equal("2", resultArray[2]);
        }

        [Fact]
        public void Pipeline_Execute_QueryHandlers_Integers_1()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayToIntQueryHandler())
                .EndWith(i => $"-{i}-");

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal("-3-", result);
        }

        [Fact]
        public void Pipeline_Execute_QueryHandlers_Integers_2()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .EndWith(new IntegerArrayToStringQueryHandler());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            var resultArray = result.Split(new[] {", "}, StringSplitOptions.None);
            Assert.Equal("0", resultArray[0]);
            Assert.Equal("1", resultArray[1]);
            Assert.Equal("2", resultArray[2]);
        }
    }
}
