namespace EtAlii.xTechnology.Tests
{
    using System;
    using EtAlii.xTechnology.Structure.Pipelines2;
    using Xunit;

    
    public class InjectablePipeline_Execution_QueryHandlers_Tests
    {
        //[Fact]
        //public void InjectablePipeline_QueryHandlers_Integers_1()
        //{
        //    // o = i * 3 * 2 + 4 + 1 + 2 * 2;

        //    // Arrange.
        //    var pipeline = new InjectablePipeline<int, string>(null)
        //        .StartWith<IntegerToArrayQueryHandler>()
        //        .EndWith(new IntegerArrayToStringQueryHandler());

        //    // Act.
        //    var result = pipeline.Process(3);

        //    // Assert.
        //    Assert.Equal("0, 1, 2", result);
        //}

        [Fact]
        public void Pipeline_Execute_QueryHandlers_Integers_2()
        {
            // o = i * 3 * 2 + 4 + 1 + 2 * 2;

            // Arrange.
            var pipeline = new Pipeline<int, string[]>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayToStringQueryHandler())
                .EndWith(s => s.Split(new[] {", "}, StringSplitOptions.None));

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal("0", result[0]);
            Assert.Equal("1", result[1]);
            Assert.Equal("2", result[2]);
        }
    }
}
