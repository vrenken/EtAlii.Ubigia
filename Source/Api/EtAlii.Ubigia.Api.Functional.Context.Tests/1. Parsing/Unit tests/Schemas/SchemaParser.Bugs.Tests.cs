namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public class SchemaParserBugsTests
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

        [Fact]
        public void SchemaParserBugs_Parse_Node_Set_Annotation()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"Person = @node(Person:Doe/John)
            {
                NickName = @node-set(""Johnny"")
            }";


            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.NotNull(parseResult.Schema.Structure.Annotation);
            var valueFragment = Assert.Single(parseResult.Schema.Structure.Values);
            Assert.NotNull(valueFragment!.Annotation);
            var assignAndSelectValueAnnotation = Assert.IsType<AssignAndSelectValueAnnotation>(valueFragment.Annotation);
            Assert.Null(assignAndSelectValueAnnotation.Source);
            Assert.NotNull(assignAndSelectValueAnnotation.Subject);
            var subject = Assert.IsType<StringConstantSubject>(assignAndSelectValueAnnotation.Subject);
            Assert.Equal("Johnny",subject.Value);
        }

        [Fact]
        public void SchemaParserBugs_Parse_Node_Clear_Annotation()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"Person = @node(Person:Doe/John)
            {
                FirstName = @node-clear()
            }";

            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.NotNull(parseResult.Schema.Structure.Annotation);
            var valueFragment = Assert.Single(parseResult.Schema.Structure.Values);
            Assert.NotNull(valueFragment!.Annotation);
            var assignAndSelectValueAnnotation = Assert.IsType<ClearAndSelectValueAnnotation>(valueFragment.Annotation);
            Assert.Null(assignAndSelectValueAnnotation.Source);
        }
    }
}
