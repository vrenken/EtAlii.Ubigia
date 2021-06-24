// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;

    public partial class SchemaParserTests
    {
        [Fact]
        public void SchemaParser_Create()
        {
            // Arrange.

            // Act.
            //var jsonNode = new SchemaParser();

            // Assert.
            //Assert.NotNull(jsonNode);
        }

        [Fact]
        public void SchemaParser_Parse_Comment()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"-- This is a comment { }";


            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.Null(parseResult.Schema);
        }

        [Fact]
        public void SchemaParser_Parse_Comment_And_Object_Single_Line()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"-- This is a comment { ""key"": ""value"" }";


            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.Null(parseResult.Schema);
        }
    }
}
