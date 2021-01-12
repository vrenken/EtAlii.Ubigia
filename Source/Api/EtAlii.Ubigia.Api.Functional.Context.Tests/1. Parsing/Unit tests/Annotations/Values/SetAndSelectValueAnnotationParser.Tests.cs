namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public class SetAndSelectValueAnnotationParserTests
    {
        [Fact]
        public void SetAndSelectValueAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private ISetAndSelectValueAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<ISetAndSelectValueAnnotationParser>();
        }

        [Fact]
        public void SetAndSelectValueAnnotationParser_Parse_Value_LastName_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-set(\\LastName, 'Does2')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
            Assert.Equal("Does2", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }


        [Fact]
        public void SetAndSelectValueAnnotationParser_Parse_Value_LastName_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-set(\\LastName, ""Does2"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
            Assert.Equal("Does2", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }


        [Fact]
        public void SetAndSelectValueAnnotationParser_Parse_Value_Integer()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-set(\\Weight, 42)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\Weight",valueAnnotation.Source.ToString());
            Assert.Equal("42", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }

        [Fact]
        public void SetAndSelectValueAnnotationParser_Parse_Value_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-set(//Nickname, 'Johnny')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal("//Nickname", valueAnnotation.Source.ToString());
            Assert.Equal("Johnny", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }

        [Fact]
        public void SetAndSelectValueAnnotationParser_Parse_Value_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-set(//Nickname, ""Johnny"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal("//Nickname", valueAnnotation.Source.ToString());
            Assert.Equal("Johnny", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }
    }
}
