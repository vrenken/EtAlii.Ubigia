namespace EtAlii.Ubigia.Api.Functional.Context.Tests
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

        [Theory, ClassData(typeof(FileBasedGraphXLData))]
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
                            FirstName @node()
                            LastName @node(\#FamilyName)
                            NickName
                            Birthdate
                            Friends @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                            {
                                FirstName @node()
                                LastName @node(\#FamilyName)
                            }
                       }";


            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            var structureFragment = parseResult.Schema.Structure;
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);
            Assert.Single(structureFragment.Children);
            var childStructure = structureFragment.Children[0];

            Assert.Equal(FragmentType.Mutation, childStructure.Type);
            Assert.NotNull(childStructure.Annotation);
            Assert.NotNull(childStructure.Annotation.Source);
            Assert.IsType<LinkAndSelectMultipleNodesAnnotation>(childStructure.Annotation);
            var linkAnnotation = (LinkAndSelectMultipleNodesAnnotation)childStructure.Annotation;
            Assert.Equal("/Friends", linkAnnotation.Source.ToString());
            Assert.Equal("Person:Banner/Peter", linkAnnotation.Target.ToString());
            Assert.Equal("/Friends", linkAnnotation.TargetLink.ToString());

            var valueFragment1 = childStructure.Values.Single(v => v.Name == "FirstName");
            Assert.NotNull(valueFragment1);
            Assert.Equal(FragmentType.Query,valueFragment1.Type);
            Assert.Null(valueFragment1.Annotation.Source);

            var valueFragment2 = childStructure.Values.Single(v => v.Name == "LastName");
            Assert.NotNull(valueFragment2);
            Assert.Equal(FragmentType.Query,valueFragment2.Type);
            Assert.Equal(@"\#FamilyName",valueFragment2.Annotation.Source.ToString());


        }
    }
}
