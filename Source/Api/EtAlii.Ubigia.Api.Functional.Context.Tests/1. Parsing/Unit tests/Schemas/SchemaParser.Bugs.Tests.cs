namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Linq;
    using Xunit;

    public partial class SchemaParserBugsTests
    {

        [Fact]
        public void SchemaParserBugs_Parse_Comment()
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

    }
}
