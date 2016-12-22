namespace EtAlii.xTechnology.Tests
{
    using Xunit;
    using EtAlii.xTechnology.Structure.Pipelines2;

    
    public class Pipeline_Create_Tests
    {
        [Fact]
        public void Pipeline_Create_FunctionBased_Int_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, int>();

            // Assert.
        }

        [Fact]
        public void Pipeline_Create_FunctionBased_Int_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int, string>();

            // Assert.
        }

        [Fact]
        public void Pipeline_Create_FunctionBased_String_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string, int>();

            // Assert.
        }


        [Fact]
        public void Pipeline_Create_ActionBased_Int()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>();

            // Assert.
        }

        [Fact]
        public void Pipeline_Create_ActionBased_String()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<int>();

            // Assert.
        }
    }
}
