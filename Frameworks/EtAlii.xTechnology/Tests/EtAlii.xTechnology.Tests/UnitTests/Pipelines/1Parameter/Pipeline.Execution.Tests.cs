namespace EtAlii.xTechnology.Tests
{
    using System;
    using EtAlii.xTechnology.Structure.Pipelines2;
    using Xunit;

    
    public class Pipeline_Execution_Tests
    {
        [Fact]
        public void Pipeline_Execute_Create()
        {
            // Arrange.
            var pipeline = new Pipeline<string, int>()
                .StartWith(AddPrefix)
                .EndWith(CountCharacters);

            // Act.
            var result = pipeline.Process("BaadFood");

            // Assert.
            Assert.Equal(14, result);
        }

        [Fact]
        public void Pipeline_Execute_Create_2()
        {
            // Arrange.
            var pipeline = new Pipeline<string, string>()
                .StartWith(AddPrefix)
                .EndWith(ToUppercase);

            // Act.
            var result = pipeline.Process("BaadFood");

            // Assert.
            Assert.Equal("FOOTERBAADFOOD", result);
        }


        [Fact]
        public void Pipeline_Execute_Create_3()
        {
            // Arrange.
            var pipeline = new Pipeline<string, bool>()
            .StartWith(AddPrefix)
            .ContinueWith(s => ToUppercase(s))
            .ContinueWith(s => s.Length)
            .EndWith(new SendToRemoteSystem());
            
            // Act.
            var result = pipeline.Process("BaadFood");

            // Assert.
            Assert.Equal(true, result);
        }

        [Fact]
        public void Pipeline_Execute_Create3()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string, double>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(c => c.Length);

            // Assert.
        }

        [Fact]
        public void Pipeline_Execute_Create2()
        {
            // Arrange.

            // Act.
            var pipeline = new Pipeline<string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(s => Console.WriteLine(s));

            // Assert.
        }

        [Fact]
        public void Pipeline_Execute_Combine_On_End()
        {
            // Arrange.
            var firstPipeline = new Pipeline<string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(new KinkyOperation())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(Console.WriteLine);

            // Act.
            var secondPipeline = new Pipeline<string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(firstPipeline);

            // Assert.
        }

        [Fact]
        public void Pipeline_Execute_Combine_On_Start()
        {
            // Arrange.
            var firstPipeline = new Pipeline<string, string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(s => s.ToUpper());

            // Act.
            var secondPipeline = new Pipeline<string>()
                .StartWith(firstPipeline)
                .ContinueWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(s => Console.WriteLine(s));

            // Assert.
        }


        [Fact]
        public void Pipeline_Execute_Combine_On_Continue()
        {
            // Arrange.
            var firstPipeline = new Pipeline<string, string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(s => s.ToUpper());

            // Act.
            var secondPipeline = new Pipeline<string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(firstPipeline)
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(s => Console.WriteLine(s));

            // Assert.
        }

        [Fact]
        public void Pipeline_Execute_Process()
        {
            // Arrange.
            var pipeline = new Pipeline<string, int>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(c => c.Length);

            // Act.
            var result = pipeline.Process("BaadFood");

            // Assert.
            Assert.Equal(15, result);
        }

        [Fact]
        public void Pipeline_Execute_Process2()
        {
            // Arrange.
            var result = 0;
            var pipeline = new Pipeline<string>()
                .StartWith(s => s.ToUpper())
                .ContinueWith(s2 => s2.ToCharArray())
                .ContinueWith(c => String.Join("-", c))
                .EndWith(s => result = s.Length);

            // Act.
            pipeline.Process("BaadFood");

            // Assert.
            Assert.Equal(15, result);
        }

        private string ToUppercase(string input)
        {
            return input.ToUpper();
        }

        private string AddPrefix(string input)
        {
            return "FOOTER" + input;
        }

        private int CountCharacters(string input)
        {
            return input.Length;
        }
    }
}
