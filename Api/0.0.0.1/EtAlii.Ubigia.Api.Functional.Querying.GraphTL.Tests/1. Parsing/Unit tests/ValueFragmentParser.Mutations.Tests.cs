namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;

    public class ValueFragmentParserMutationsTests 
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
        public void ValueFragmentParser_Parse_Value_Mutation_Space()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "firstname <= \"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }
        
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Tab()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "firstname\t<=\t\"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }
        
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Compact()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "firstname<=\"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Space()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "Location <= @value(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", annotation.Path.ToString());
        }
        
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Compact()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "Location<=@value(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", annotation.Path.ToString());
        }

                
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Tab()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "Location\t<=\t@value(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", annotation.Path.ToString());
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Space()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "FirstName @value() <= \"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Path);
            Assert.Equal("John", valueFragment.Mutation.ToString());
        }
                
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Tab()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "FirstName @value()\t<=\t\"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Path);
            Assert.Equal("John", valueFragment.Mutation.ToString());
        }
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Compact()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "FirstName @value()<=\"John\"";
            
            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value, annotation.Type);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Path);
            Assert.Equal("John", valueFragment.Mutation.ToString());
        }
    }
}
