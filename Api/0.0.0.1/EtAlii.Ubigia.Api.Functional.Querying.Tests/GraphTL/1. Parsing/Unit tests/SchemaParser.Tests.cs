namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
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
        
        [Theory, ClassData(typeof(FileBasedGraphTLData))]
        public void SchemaParser_Parse_From_Files(string fileName, string title, string queryText)
        {
            // Arrange.
#pragma warning disable 1717
            title = title;
            fileName = fileName;
#pragma warning restore 1717
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            
            // Act.
            var parseResult = parser.Parse(queryText);
            var lines = queryText.Split('\n');
            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);

            // Let's not assert the query if we don't have one in the original script.
            var noCode = lines.All(line => line.StartsWith("--") || string.IsNullOrWhiteSpace(line));
            if (!noCode)
            {
                Assert.NotNull(parseResult.Schema);
            }
        }

        [Fact]
        public void SchemaParser_Parse_Comment()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            var normalPersonText = @"-- This is a comment { }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.Null(parseResult.Schema);
        }

        [Fact]
        public void SchemaParser_Parse_Comment_And_Object_Single_Line()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            var normalPersonText = @"-- This is a comment { ""key"": ""value"" }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.Null(parseResult.Schema);
        }

        
        [Fact]
        public void SchemaParser_Parse_Comment_MultiLine_And_Object()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            var text = @"-- This is a comment on the first line.
            -- And this is a comment on the second line.
            Person
            { 
                ""key"" <= ""value"" 
            }";
            
            
            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
        }


    }
}
