// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class Pipeline2StartWithTests
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
