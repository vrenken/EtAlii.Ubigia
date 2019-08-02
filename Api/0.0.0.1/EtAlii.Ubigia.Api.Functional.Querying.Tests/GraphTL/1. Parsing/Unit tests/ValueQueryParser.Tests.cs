namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;

    public class ValueQueryParserTests 
    {
        [Fact]
        public void ValueQueryParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateValueQueryParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IValueQueryParser CreateValueQueryParser() => new SchemaParserTestContainerFactory().Create().GetInstance<IValueQueryParser>(); 

        [Fact]
        public void ValueQueryParser_Parse_Without_Annotation()
        {
            // Arrange.
            var parser = CreateValueQueryParser();
            var text = @"firstname";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueQuery = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueQuery.Annotation;
            Assert.Null(annotation);
        }
        
        [Fact]
        public void ValueQueryParser_Parse_With_Value_Annotation()
        {
            // Arrange.
            var parser = CreateValueQueryParser();
            var text = @"firstname @value()";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueQuery = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueQuery.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Null(annotation.Path);
        }
        
        [Fact]
        public void ValueQueryParser_Parse_With_Relative_Value_Annotation()
        {
            // Arrange.
            var parser = CreateValueQueryParser();
            var text = @"lastname @value(\\)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueQuery = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueQuery.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal(@"\\", annotation.Path.ToString());
        }
    }
}
