// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineStartWithTests
    {
        /// Delegate based ==================================================================================

        [Fact]
        public void Pipeline_StartWith_FunctionBased_Delegate_Int_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int, int>();

            // Act.
            var registration = pipeline.StartWith(i => i + 1);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_Delegate_Int_String()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>();

            // Act.
            var registration = pipeline.StartWith(i => i + 1);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_Delegate_String_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<string, int>();

            // Act.
            var registration = pipeline.StartWith(i => i + 1);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string, string>>(registration);
        }


        [Fact]
        public void Pipeline_StartWith_ActionBased_Delegate_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int>();

            // Act.
            var registration = pipeline.StartWith(i => i + 1);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_ActionBased_Delegate_String()
        {
            // Arrange.
            var pipeline = new Pipeline<string>();

            // Act.
            var registration = pipeline.StartWith(i => i + 1);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string>>(registration);
        }

        /// Interface based ==================================================================================

        [Fact]
        public void Pipeline_StartWith_FunctionBased_Interface_Int_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int, int>();

            // Act.
            var registration = pipeline.StartWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_Interface_Int_String()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>();

            // Act.
            var registration = pipeline.StartWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_Interface_String_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<string, int>();

            // Act.
            var registration = pipeline.StartWith(new StringActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string, string>>(registration);
        }


        [Fact]
        public void Pipeline_StartWith_ActionBased_Interface_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int>();

            // Act.
            var registration = pipeline.StartWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_ActionBased_Interface_String()
        {
            // Arrange.
            var pipeline = new Pipeline<string>();

            // Act.
            var registration = pipeline.StartWith(new StringActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string>>(registration);
        }

        /// QueryHandler based ==================================================================================

        [Fact]
        public void Pipeline_StartWith_FunctionBased_QueryHandler_Int_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int, int>();

            // Act.
            var registration = pipeline.StartWith(new IntegerToArrayQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int[]>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_QueryHandler_Int_String()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>();

            // Act.
            var registration = pipeline.StartWith(new IntegerToArrayQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int[]>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_QueryHandler_String_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<string, int>();

            // Act.
            var registration = pipeline.StartWith(new StringToArrayQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string, string[]>>(registration);
        }


        [Fact]
        public void Pipeline_StartWith_ActionBased_QueryHandler_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int>();

            // Act.
            var registration = pipeline.StartWith(new IntegerToArrayQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int[]>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_ActionBased_QueryHandler_String()
        {
            // Arrange.
            var pipeline = new Pipeline<string>();

            // Act.
            var registration = pipeline.StartWith(new StringToArrayQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string[]>>(registration);
        }

        /// CommandHandler based ==================================================================================

        [Fact]
        public void Pipeline_StartWith_FunctionBased_CommandHandler_Int_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<int, int>();

            // Act.
            var registration = pipeline.StartWith(new IntegerCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_CommandHandler_Int_String()
        {
            // Arrange.
            var pipeline = new Pipeline<int, string>();

            // Act.
            var registration = pipeline.StartWith(new IntegerCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_FunctionBased_CommandHandler_String_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<string, int>();

            // Act.
            var registration = pipeline.StartWith(new StringCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string, string>>(registration);
        }


        [Fact]
        public void Pipeline_StartWith_ActionBased_CommandHandler_Int()
        {
            // Arrange.
            var pipeline = new Pipeline<string>();

            // Act.
            var registration = pipeline.StartWith(new StringCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string>>(registration);
        }

        [Fact]
        public void Pipeline_StartWith_ActionBased_CommandHandler_String()
        {
            // Arrange.
            var pipeline = new Pipeline<string>();

            // Act.
            var registration = pipeline.StartWith(new StringCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string>>(registration);
        }

    }
}
