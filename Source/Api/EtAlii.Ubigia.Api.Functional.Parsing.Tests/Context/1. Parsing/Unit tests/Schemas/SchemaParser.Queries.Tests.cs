// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public partial class SchemaParserTests
    {
        [Fact]
        public void SchemaParser_Parse_Query_Flat_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person
            {
                ""age"",
                ""first"",
                ""last"",
                ""company"",
                ""email"",
                ""phone""
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText, scope);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person
            {
                age,
                first,
                last,
                company,
                email,
                phone
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText, scope);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person
            {
                age
                first
                last
                company
                email
                phone
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText, scope);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person
            {
                age, first, last, company, email, phone
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText, scope);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }
    }
}
