namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines2;
    using Xunit;

    
    public class Pipeline_Execution_ActionBased_Tests
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
