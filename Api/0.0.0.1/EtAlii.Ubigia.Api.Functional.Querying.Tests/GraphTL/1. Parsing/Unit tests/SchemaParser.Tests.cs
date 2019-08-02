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
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            var text = @"-- This is a comment { ""key"": ""value"" }";
            
            
            // Act.
            var parseResult = parser.Parse(text);

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
                
        [Fact]
        public void SchemaParser_Parse_Nested_Mutation()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            var text = @"Person @nodes(Person:Doe/John)
                       {
                            FirstName @value()
                            LastName @value(\#FamilyName)
                            Nickname
                            Birthdate
                            Friends @nodes(/Friends += Person:Vrenken/Peter)
                            {
                                FirstName @value()
                                LastName @value(\#FamilyName)
                            }
                       }";
            
            
            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureQuery>(parseResult.Schema.Structure);
            var structureQuery = (StructureQuery)parseResult.Schema.Structure;
            Assert.Single(structureQuery.Children);
            var child = structureQuery.Children[0];

            Assert.IsType<StructureMutation>(child);
            var structureMutation = (StructureMutation) child;
            Assert.NotNull(structureMutation.Annotation);
            Assert.NotNull(structureMutation.Annotation.Path);
            Assert.NotNull(structureMutation.Annotation.Operator);
            Assert.NotNull(structureMutation.Annotation.Subject);
            Assert.Equal(AnnotationType.Nodes,structureMutation.Annotation.Type);
            Assert.Equal("/Friends", structureMutation.Annotation.Path.ToString());
            Assert.Equal(" += ", structureMutation.Annotation.Operator.ToString());
            Assert.Equal("Person:Vrenken/Peter", structureMutation.Annotation.Subject.ToString());
            
            var valueQuery1 = structureMutation.Values.Single(v => v.Name == "FirstName") as ValueQuery; 
            Assert.NotNull(valueQuery1);
            Assert.Equal(AnnotationType.Value,valueQuery1.Annotation.Type);
            Assert.Null(valueQuery1.Annotation.Path);

            var valueQuery2 = structureMutation.Values.Single(v => v.Name == "LastName") as ValueQuery; 
            Assert.NotNull(valueQuery2);
            Assert.Equal(AnnotationType.Value,valueQuery2.Annotation.Type);
            Assert.Equal(@"\#FamilyName",valueQuery2.Annotation.Path.ToString());
            

        }
    }
}
