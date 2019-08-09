namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;

    public class ValueFragmentParserQueriesTests 
    {
        [Fact]
        public void ValueFragmentParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateValueFragmentParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IValueFragmentParser CreateValueFragmentParser() => new SchemaParserTestContainerFactory().Create().GetInstance<IValueFragmentParser>(); 

        [Fact]
        public void ValueFragmentParser_Parse_Query_Without_Annotation()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = @"firstname";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
        }
        
        [Fact]
        public void ValueFragmentParser_Parse_Query_With_Value_Annotation()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = @"firstname @value()";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Null(annotation.Path);
        }
        
        [Fact]
        public void ValueFragmentParser_Parse_Query_With_Relative_Value_Annotation()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = @"lastname @value(\\)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);
            
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal(@"\\", annotation.Path.ToString());
        }
    }
}
