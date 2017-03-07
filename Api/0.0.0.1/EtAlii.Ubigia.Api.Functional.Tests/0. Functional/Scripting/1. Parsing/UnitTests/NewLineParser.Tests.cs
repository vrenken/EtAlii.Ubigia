namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    
    using Moppet.Lapa;
    using Xunit;


    public class NewLineParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Create()
        {
            // Arrange.

            // Act.
            var parser = new NewLineParser();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(String.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Leading_Space()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do(" \n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(String.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Following_Space()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n ");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(String.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Leading_And_Following_Space()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do(" \n ");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(String.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n\n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(String.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple_With_Space_Inbetween()
        {
            // Arrange.
            var parser = new NewLineParser();

            // Act.
            var result = parser.OptionalMultiple.Do("\n \n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(String.Empty, result.Rest.ToString());
        }
    }
}