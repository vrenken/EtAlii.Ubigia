// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineContinueWithTests
    {
        /// Delegate based ==================================================================================

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_Delegate_Int_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, int>()
                .StartWith(i => i + 1)
                .ContinueWith(i => i * 3);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_Delegate_Int_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, string>()
                .StartWith(i => i + 1)
                .ContinueWith(i => i * 3);

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_Delegate_String_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string, int>()
                .StartWith(i => i + 1)
                .ContinueWith(i => i.ToUpper());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string, string>>(registration);
        }


        [Fact]
        public void Pipeline_ContinueWith_ActionBased_Delegate_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int>()
                .StartWith(i => i + 1)
                .ContinueWith(_ => { });

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_ActionBased_Delegate_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string>()
                .StartWith(i => i + 1)
                .ContinueWith(_ => { });

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string>>(registration);
        }

        /// Interface based ==================================================================================

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_Interface_Int_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, int>()
                .StartWith(new IntegerActionBasedOperation())
                .ContinueWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_Interface_Int_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, string>()
                .StartWith(new IntegerActionBasedOperation())
                .ContinueWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_Interface_String_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string, int>()
                .StartWith(new StringActionBasedOperation())
                .ContinueWith(new StringActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string, string>>(registration);
        }


        [Fact]
        public void Pipeline_ContinueWith_ActionBased_Interface_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int>()
                .StartWith(new IntegerActionBasedOperation())
                .ContinueWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_ActionBased_Interface_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string>()
                .StartWith(new StringActionBasedOperation())
                .ContinueWith(new StringActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string, string>>(registration);
        }

        /// QueryHandler based ==================================================================================

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_QueryHandler_Int_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, int>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayToIntQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int[], int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_QueryHandler_Int_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayToIntQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, int[], int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_QueryHandler_String_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string, int>()
                .StartWith(new StringToArrayQueryHandler())
                .ContinueWith(new StringArrayToIntQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, string[], int>>(registration);
        }


        [Fact]
        public void Pipeline_ContinueWith_ActionBased_QueryHandler_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int>()
                .StartWith(new IntegerToArrayQueryHandler())
                .ContinueWith(new IntegerArrayToIntQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int[], int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_ActionBased_QueryHandler_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string>()
                .StartWith(new StringToArrayQueryHandler())
                .ContinueWith((new StringArrayToIntQueryHandler()));

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, string[], int>>(registration);
        }

        /// CommandHandler based ==================================================================================

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_CommandHandler_Int_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, int>()
                .StartWith(new IntegerCommandHandler())
                .ContinueWith(new IntegerCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, int, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_CommandHandler_Int_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int, string>()
                .StartWith(new IntegerToStringQueryHandler())
                .ContinueWith(new StringCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, string, string>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_FunctionBased_CommandHandler_String_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string, int>()
                .StartWith(new StringToIntQueryHandler())
                .ContinueWith(new IntegerCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, int, int>>(registration);
        }


        [Fact]
        public void Pipeline_ContinueWith_ActionBased_CommandHandler_Int()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<string>()
                .StartWith(new StringToIntQueryHandler())
                .ContinueWith(new IntegerCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<string, int, int>>(registration);
        }

        [Fact]
        public void Pipeline_ContinueWith_ActionBased_CommandHandler_String()
        {
            // Arrange.

            // Act.
            var registration = new Pipeline<int>()
                .StartWith(new IntegerToStringQueryHandler())
                .ContinueWith(new StringCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IOperationRegistration<int, string, string>>(registration);
        }
    }
}
