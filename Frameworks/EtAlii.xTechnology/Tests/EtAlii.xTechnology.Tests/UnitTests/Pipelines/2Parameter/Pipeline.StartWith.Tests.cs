namespace EtAlii.xTechnology.Tests
{
    using Xunit;
    using EtAlii.xTechnology.Structure.Pipelines2;

    
    public class Pipeline2_StartWith_Tests
    {
        /// QueryHandler based ==================================================================================

        [Fact]
        public void Pipeline2_StartWith_FunctionBased_QueryHandler_Ints_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<IntegersToStringQuery, int>();

            // Act.
            var registration = pipeline.StartWith(new IntegersToStringQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<IntegersToStringQuery, int, IntegersToStringQuery, string>>(registration);
        }
    }
}
