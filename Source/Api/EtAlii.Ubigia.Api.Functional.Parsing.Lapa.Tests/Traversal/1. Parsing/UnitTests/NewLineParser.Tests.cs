// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;
    using Xunit;

    public class NewLineParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Create()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();

            // Act.
            var parser = new NewLineParser(whitespaceParser);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Leading_Space()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = new LpsParser(parser.Optional).Do(" \n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Following_Space()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n ");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_With_Leading_And_Following_Space()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = new LpsParser(parser.Optional).Do(" \n ");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = new LpsParser(parser.Optional).Do("\n\n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple_With_Space_Inbetween()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = parser.OptionalMultiple.Do("\n \n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NewLineParser_Single_Newline_Multiple_With_Tab_Inbetween()
        {
            // Arrange.
            var whitespaceParser = new WhitespaceParser();
            var parser = new NewLineParser(whitespaceParser);

            // Act.
            var result = parser.OptionalMultiple.Do("\n\t\n");

            // Assert.
            Assert.True(result.Success);
            Assert.Equal(string.Empty, result.Rest.ToString());
        }
    }
}
