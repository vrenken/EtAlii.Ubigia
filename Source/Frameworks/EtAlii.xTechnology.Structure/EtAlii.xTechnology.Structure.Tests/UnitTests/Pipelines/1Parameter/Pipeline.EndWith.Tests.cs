// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;
    using Xunit;

    public class PipelineEndWithTests
    {
        /// Delegate based ==================================================================================

        [Fact]
        public void Pipeline_EndWith_FunctionBased_Delegate_Int_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, int>()
                                .StartWith(i => i + 1)
                                .EndWith(i => i + 1);

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int, int>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_FunctionBased_Delegate_Int_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, string>()
                                .StartWith(i => i + 1)
                                .EndWith(i => i.ToString());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int, string>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_FunctionBased_Delegate_String_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string, int>()
                .StartWith(s => s + "Postfix")
                .EndWith(s => s.Length);

            // Assert.
            Assert.IsAssignableFrom<IPipeline<string, int>>(pipeline);
        }


        [Fact]
        public void Pipeline_EndWith_ActionBased_Delegate_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>()
                .StartWith(i => i + 1)
                .EndWith(_ => { });

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_ActionBased_Delegate_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string>()
                .StartWith(s => s + "Postfix")
                .EndWith(_ => { });

            // Assert.
            Assert.IsAssignableFrom<IPipeline<string>>(pipeline);
        }

        /// Interface based ==================================================================================

        [Fact]
        public void Pipeline_EndWith_FunctionBased_Interface_Int_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, int>()
                .StartWith(new IntegerActionBasedOperation())
                .EndWith(new IntegerActionBasedOperation());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int, int>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_FunctionBased_Interface_Int_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerActionBasedOperation())
                .EndWith(new IntegerToStringOperation());
            

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int, string>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_FunctionBased_Interface_String_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string, int>()
                .StartWith(new StringActionBasedOperation())
                .EndWith(new StringToIntegerOperation());
            

            // Assert.
            Assert.IsAssignableFrom<IPipeline<string, int>>(pipeline);
        }


        [Fact]
        public void Pipeline_EndWith_ActionBased_Interface_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>()
                .StartWith(new IntegerToStringOperation())
                .EndWith(new StringOperation());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_ActionBased_Interface_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string>()
                .StartWith(new StringActionBasedOperation())
                .EndWith(new StringOperation());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<string>>(pipeline);
        }

        /// QueryHandler based ==================================================================================

        [Fact]
        public void Pipeline_EndWith_FunctionBased_QueryHandler_Int_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, int>()
                .StartWith(new IntegerToArrayQueryHandler())
                .EndWith(new IntegerArrayToIntQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int, int>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_FunctionBased_QueryHandler_Int_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, string>()
                .StartWith(new IntegerToArrayQueryHandler())
                .EndWith(new IntegerArrayToStringQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int, string>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_FunctionBased_QueryHandler_String_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string, int>()
                .StartWith(new StringToArrayQueryHandler())
                .EndWith(new StringArrayToIntQueryHandler());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<string, int>>(pipeline);
        }


        /// CommandHandler based ==================================================================================

        [Fact]
        public void Pipeline_EndWith_ActionBased_CommandHandler_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>()
                .StartWith(new IntegerCommandHandler())
                .EndWith(new IntegerCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<int>>(pipeline);
        }

        [Fact]
        public void Pipeline_EndWith_ActionBased_CommandHandler_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string>()
                .StartWith(new StringCommandHandler())
                .EndWith(new StringCommandHandler());

            // Assert.
            Assert.IsAssignableFrom<IPipeline<string>>(pipeline);
        }

    }
}
