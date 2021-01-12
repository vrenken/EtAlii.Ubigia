namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class ClearAndSelectValueAnnotationParserTests
    {
        [Fact]
        public void ClearAndSelectValueAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IClearAndSelectValueAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IClearAndSelectValueAnnotationParser>();
        }

        [Fact]
        public void ClearAndSelectValueAnnotationParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-clear(\\LastName)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
        }

        [Fact]
        public void ClearAndSelectValueAnnotationParser_Parse_Value_NickName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-clear(NickName)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"NickName",valueAnnotation.Source.ToString());
        }

        [Fact]
        public void ClearAndSelectValueAnnotationParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-clear(//Weight)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"//Weight",valueAnnotation.Source.ToString());
        }
    }
}
