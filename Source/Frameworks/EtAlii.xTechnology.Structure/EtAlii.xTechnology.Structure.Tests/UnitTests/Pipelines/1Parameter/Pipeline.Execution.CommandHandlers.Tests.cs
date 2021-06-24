// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using System;
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineExecutionCommandHandlersTests
    {
        [Fact]
        public void Pipeline_Execute_CommandHandlers_Integers_1()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            int[] result2 = null;
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayOutputCommandHandler(output => result2 = output))
                .EndWith(new IntegerArrayToStringQueryHandler());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            var resultArray = result.Split(new[] { ", " }, StringSplitOptions.None);
            Assert.Equal("0", resultArray[0]);
            Assert.Equal("1", resultArray[1]);
            Assert.Equal("2", resultArray[2]);

            Assert.Equal(0, result2[0]);
            Assert.Equal(1, result2[1]);
            Assert.Equal(2, result2[2]);
        }

        [Fact]
        public void Pipeline_Execute_CommandHandlers_Integers_2()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            int[] result2 = null;
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayOutputCommandHandler(output => result2 = output))
                .EndWith(new IntegerArrayToStringQueryHandler());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal("0, 1, 2", result);

            Assert.Equal(0, result2[0]);
            Assert.Equal(1, result2[1]);
            Assert.Equal(2, result2[2]);
        }

        [Fact]
        public void Pipeline_Execute_CommandHandlers_Integers_3()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            int[] result2 = null;
            var pipeline = new Pipeline<int>()
                .StartWith(new IntegerToArrayQueryHandler())
                .EndWith(new IntegerArrayOutputCommandHandler(output => result2 = output));


            // Act.
            pipeline.Process(3);

            // Assert.
            Assert.Equal(0, result2[0]);
            Assert.Equal(1, result2[1]);
            Assert.Equal(2, result2[2]);
        }


        [Fact]
        public void Pipeline_Execute_CommandHandlers_Integers_4()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            int[] result2 = null;
            var pipeline = new Pipeline<int>()
                .StartWith(new IntegerToArrayQueryHandler())
                .EndWith(new IntegerArrayOutputCommandHandler(output => result2 = output));

            // Act.
            pipeline.Process(3);

            // Assert.
            Assert.Equal(0, result2[0]);
            Assert.Equal(1, result2[1]);
            Assert.Equal(2, result2[2]);
        }

    }
}
