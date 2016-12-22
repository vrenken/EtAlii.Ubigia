namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines2;
    using Xunit;

    
    public class Pipeline_Execution_FunctionBased_Tests
    {
        [Fact]
        public void Pipeline_Execute_FunctionBased_Integers_1()
        {
            // Arrange.
            var pipeline = new Pipeline<int, int>()
                .StartWith(i => i + 2)
                .EndWith(i => i * 4);

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal(20, result);
        }

        [Fact]
        public void Pipeline_Execute_FunctionBased_Integers_2()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>()
                .StartWith(i => i + 2)
                .ContinueWith(i => i * 4)
                .EndWith(i => i.ToString());

            // Act.
            var result = pipeline.Process(3);

            // Assert.
            Assert.Equal("20", result);
        }
    }
}
