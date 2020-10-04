namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class ClearAndSelectNodeValueAnnotationParserTests 
    {
        [Fact]
        public void ClearAndSelectNodeValueAnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IClearAndSelectNodeValueAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IClearAndSelectNodeValueAnnotationParser>();
        }
        
        [Fact]
        public void ClearAndSelectNodeValueAnnotationParser_Parse_Value_LastName() 
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
            var valueAnnotation = annotation as ClearAndSelectNodeValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString()); 
        }

        [Fact]
        public void ClearAndSelectNodeValueAnnotationParser_Parse_Value_NickName()
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
            var valueAnnotation = annotation as ClearAndSelectNodeValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"NickName",valueAnnotation.Source.ToString());
        }

        [Fact]
        public void ClearAndSelectNodeValueAnnotationParser_Parse_Value()
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
            var valueAnnotation = annotation as ClearAndSelectNodeValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"//Weight",valueAnnotation.Source.ToString());
        }
    }
}
