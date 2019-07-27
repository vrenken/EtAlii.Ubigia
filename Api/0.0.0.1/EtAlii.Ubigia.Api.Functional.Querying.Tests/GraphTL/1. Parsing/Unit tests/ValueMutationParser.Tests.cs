namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;

    public class ValueMutationParserTests 
    {
        [Fact]
        public void ValueMutationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateValueMutationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IValueMutationParser CreateValueMutationParser() => new QueryParserTestContainerFactory().Create().GetInstance<IValueMutationParser>();
        
        [Fact]
        public void ValueMutationParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = @"firstname <= ""John""";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueMutation);
            Assert.Equal("John",valueMutation.Value);
        }
        
        [Fact]
        public void ValueMutationParser_Parse_Value_Correct_Assignment_Operator()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = @"firstname <= ""John""";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueMutation);
            Assert.Equal("John",valueMutation.Value);
        }
        
        [Fact]
        public void ValueMutationParser_Parse_Value_Relative()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = @"lastname <= @value(\\)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal(@"\\", annotation.Path.ToString());
        }
    }
}
