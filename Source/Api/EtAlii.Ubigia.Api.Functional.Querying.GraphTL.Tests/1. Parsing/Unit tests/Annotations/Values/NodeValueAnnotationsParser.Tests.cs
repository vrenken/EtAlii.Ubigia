namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class NodeValueAnnotationsParserTests 
    {
        [Fact]
        public void NodeValueAnnotationsParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateNodeValueAnnotationsParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private INodeValueAnnotationsParser CreateNodeValueAnnotationsParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<INodeValueAnnotationsParser>();
        }
        
        [Fact]
        public void NodeValueAnnotationsParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateNodeValueAnnotationsParser();
            var text = @"@node(\\LastName)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectNodeValueAnnotation>(annotation);
            Assert.NotNull(annotation.Source);
            Assert.Equal(@"\\LastName",annotation.Source.ToString());
        }
        
        [Fact]
        public void NodeValueAnnotationsParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateNodeValueAnnotationsParser();
            var text = @"@node()";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectNodeValueAnnotation>(annotation);
            Assert.Null(annotation.Source);
        }
    }
}
