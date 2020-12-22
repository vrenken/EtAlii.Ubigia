namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class SelectNodeValueAnnotationParserTests
    {
        [Fact]
        public void SelectNodeValueAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private ISelectNodeValueAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<ISelectNodeValueAnnotationParser>();
        }

        [Fact]
        public void SelectNodeValueAnnotationParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node(\\LastName)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as SelectNodeValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
        }

        [Fact]
        public void SelectNodeValueAnnotationParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node()";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as SelectNodeValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Null(valueAnnotation.Source);
        }
    }
}
