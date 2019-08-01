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
        public void ValueMutationParser_Parse_Value_Space()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "firstname <= \"John\"";
            
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
        public void ValueMutationParser_Parse_Value_Tab()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "firstname\t<=\t\"John\"";
            
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
        public void ValueMutationParser_Parse_Value_Compact()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "firstname<=\"John\"";
            
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
        public void ValueMutationParser_Parse_Value_From_Annotation_Space()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "Location <= @value(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("Location",valueMutation.Name);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", annotation.Path.ToString());
        }
        
        [Fact]
        public void ValueMutationParser_Parse_Value_From_Annotation_Compact()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "Location<=@value(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("Location",valueMutation.Name);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", annotation.Path.ToString());
        }

                
        [Fact]
        public void ValueMutationParser_Parse_Value_From_Annotation_Tab()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "Location\t<=\t@value(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("Location",valueMutation.Name);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", annotation.Path.ToString());
        }

        [Fact]
        public void ValueMutationParser_Parse_Value_Assignment_From_Constant_Space()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "FirstName @value() <= \"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("FirstName",valueMutation.Name);
            Assert.Null(annotation.Path);
            Assert.Equal("John", valueMutation.Value.ToString());
        }
                
        [Fact]
        public void ValueMutationParser_Parse_Value_Assignment_From_Constant_Tab()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "FirstName @value()\t<=\t\"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("FirstName",valueMutation.Name);
            Assert.Null(annotation.Path);
            Assert.Equal("John", valueMutation.Value.ToString());
        }
        [Fact]
        public void ValueMutationParser_Parse_Value_Assignment_From_Constant_Compact()
        {
            // Arrange.
            var parser = CreateValueMutationParser();
            var text = "FirstName @value()<=\"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueMutation.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("FirstName",valueMutation.Name);
            Assert.Null(annotation.Path);
            Assert.Equal("John", valueMutation.Value.ToString());
        }
    }
}
