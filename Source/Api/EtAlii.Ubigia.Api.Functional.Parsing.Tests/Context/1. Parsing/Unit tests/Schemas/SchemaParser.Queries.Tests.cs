namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;

    public partial class SchemaParserTests
    {
        [Fact]
        public void SchemaParser_Parse_Query_Flat_01()
        {
            // Arrange.
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
            var parseResult = parser.Parse(normalPersonText);

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
            var parseResult = parser.Parse(normalPersonText);

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
            var parseResult = parser.Parse(normalPersonText);

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
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person
            {
                age, first, last, company, email, phone
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }
    }
}
