// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public partial class SchemaParserTests
    {
        [Theory, ClassData(typeof(MdFileBasedGraphContextData))]
        public void SchemaParser_Parse_From_MdFiles(string fileName, string line, string queryText)
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();

            // Act.
            var parseResult = parser.Parse(queryText);
            var lines = queryText.Split('\n');

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(line);
            Assert.NotNull(fileName);

            // Let's not assert the query if we don't have one in the original script.
            var noCode = lines.All(l => l.StartsWith("--") || string.IsNullOrWhiteSpace(l));
            if (!noCode)
            {
                Assert.NotNull(parseResult.Schema);
            }
        }

        [Fact]
        public void SchemaParser_Parse_Comment_MultiLine_And_Object()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"-- This is a comment on the first line.
            -- And this is a comment on the second line.
            Person
            {
                ""key"" = ""value""
            }";


            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
        }

        [Fact]
        public void SchemaParser_Parse_Comment_Object_With_Namespace_Option()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"[namespace=EtAlii.Namespace.Test]
            Person
            {
                ""key"" = ""value""
            }";


            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.Equal("EtAlii.Namespace.Test", parseResult.Schema.Namespace);
        }
    }
}
